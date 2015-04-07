using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAbility
{
    public class AutoAbility : IPlugin
    {
        private bool _enabled;
        private int _cooldown = 20;
        private Dictionary<Client, UseItemPacket> _useItemMap = new Dictionary<Client, UseItemPacket>();
        private Classes[] _validClasses = { Classes.Rogue, Classes.Priest, Classes.Paladin, Classes.Warrior };

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Auto Ability"; }

        public string GetDescription()
        { return "Automatically uses your abilities based on your class and your specified conditions:\n" +
                 "Paladin: Automatically Seal Buff\n" +
                 "Priest: Automatically Tome Buff and/or Heal\n" + 
                 "Warrior: Automatically Helm Buff"; }

        public string[] GetCommands()
        { return new string[] { "/aa on", "/aa off", "/aa settings" }; }

        public void Initialize(Proxy proxy)
        {
            _enabled = AutoAbilityConfig.Default.EnabledByDefault;

            proxy.ClientConnected += (c) => _useItemMap.Add(c, null);
            proxy.ClientDisconnected += (c) => _useItemMap.Remove(c);

            proxy.HookPacket(PacketType.CREATESUCCESS, OnCreateSuccess);
            proxy.HookPacket(PacketType.USEITEM, OnUseItem);
            proxy.HookPacket(PacketType.UPDATE, OnUpdate);
            proxy.HookPacket(PacketType.NEWTICK, OnUpdate);
            proxy.HookCommand("aa", OnAACommand);
        }

        private void OnCreateSuccess(Client client, Packet packet)
        {
            if (_enabled)
                PluginUtils.Delay(2100, () =>
                    client.SendToClient(PluginUtils.CreateOryxNotification(
                        "Auto Ability", "Use your ability to Activate Auto Ability")));
        }

        private void OnUseItem(Client client, Packet packet)
        {
            if (_useItemMap[client] == null && _validClasses.Contains(client.PlayerData.Class))
                client.SendToClient(PluginUtils.CreateNotification(
                    client.ObjectId, "Auto Ability Activated!"));
            _useItemMap[client] = packet as UseItemPacket;
        }

        private void OnUpdate(Client client, Packet packet)
        {
            if (_cooldown > 0) _cooldown--;
            if (_cooldown != 0) return;
            if (!_enabled) return;
            if (_useItemMap[client] == null) return;

            float manaPercentage = (float)client.PlayerData.Mana / (float)client.PlayerData.MaxMana;
            float healthPercentage = (float)client.PlayerData.Health / (float)client.PlayerData.MaxHealth;

            switch (client.PlayerData.Class)
            {
                case Classes.Paladin:
                {
                    if (!client.PlayerData.HasConditionEffect(ConditionEffects.Damaging) &&
                        manaPercentage > AutoAbilityConfig.Default.RequiredManaPercent &&
                        AutoAbilityConfig.Default.PaladinAutoBuff)
                    {
                        SendUseItem(client);
                    }
                    break;
                }
                case Classes.Priest:
                {
                    if ((!client.PlayerData.HasConditionEffect(ConditionEffects.Healing) &&
                        manaPercentage > AutoAbilityConfig.Default.RequiredManaPercent &&
                        AutoAbilityConfig.Default.PriestAutoBuff) ||
                        (healthPercentage < AutoAbilityConfig.Default.RequiredHealthPercent &&
                        AutoAbilityConfig.Default.PriestAutoHeal))
                    {
                        SendUseItem(client);
                    }
                    break;
                }
                case Classes.Warrior:
                {
                    if (!client.PlayerData.HasConditionEffect(ConditionEffects.Berserk) &&
                        manaPercentage > AutoAbilityConfig.Default.RequiredManaPercent &&
                        AutoAbilityConfig.Default.WarriorAutoBuff)
                    {
                        SendUseItem(client);
                    }
                    break;
                }
                case Classes.Rogue:
                {
                    if (!client.PlayerData.HasConditionEffect(ConditionEffects.Invisible) &&
                        manaPercentage > AutoAbilityConfig.Default.RequiredManaPercent &&
                        AutoAbilityConfig.Default.RogueAutoCloak)
                    {
                        SendUseItem(client);
                    }
                    break;
                }
            }
        }

        private void OnAACommand(Client client, string command, string[] args)
        {
            if (args.Length == 0) return;

            if (args[0] == "on")
            {
                _enabled = true;
                client.SendToClient(
                    PluginUtils.CreateNotification(
                        client.ObjectId, "Auto Ability Enabled!"));
            }
            else if (args[0] == "off")
            {
                _enabled = false;
                client.SendToClient(
                    PluginUtils.CreateNotification(
                        client.ObjectId, "Auto Ability Disabled!"));
            }
            else if (args[0] == "settings")
            {
                PluginUtils.ShowGenericSettingsGUI(AutoAbilityConfig.Default, "Auto Ablity Settings");
            }
            else if (args[0] == "test")
            {
                SendUseItem(client);
            }
        }

        private void SendUseItem(Client client)
        {
            _cooldown = AutoAbilityConfig.Default.RetryDelay;

            UseItemPacket useItem = _useItemMap[client];
            useItem.Time = client.Time;
            useItem.ItemUsePos = client.PlayerData.Pos;
            client.SendToServer(useItem);

            if (AutoAbilityConfig.Default.ShowBuffNotifications)
                client.SendToClient(PluginUtils.CreateNotification(
                    client.ObjectId, "Auto-Buff Triggered!"));
        }
    }
}
