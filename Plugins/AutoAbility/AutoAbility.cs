using LibKRelay;
using LibKRelay.Messages;
using LibKRelay.Messages.Client;
using LibKRelay.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAbility
{
    public class AutoAbility : Plugin
    {
        private bool _enabled;
        private int _cooldown = 20;
        private Dictionary<Connection, UseItem> _useItemMap = new Dictionary<Connection, UseItem>();
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

        public void Initialize(ClientListener listener)
        {
            _enabled = AutoAbilityConfig.Default.EnabledByDefault;

            listener.ClientConnected += (c) => _useItemMap.Add(c, null);
            listener.ClientDisconnected += (c) => _useItemMap.Remove(c);

            Message.Hook<CreateSuccess>(OnCreateSuccess);
            Message.Hook<UseItem>(OnUseItem);
            Message.Hook<Update>(OnUpdate);
            Message.Hook<NewTick>(OnNewTick);
            //proxy.HookCommand("aa", OnAACommand);
        }

        private void OnCreateSuccess(Connection con, CreateSuccess createSuccess)
        {
            if (_enabled)
                Delay(2100, () =>
                    con.Client.Send(CreateOryxNotification(
                        "Auto Ability", "Use your ability to Activate Auto Ability")));
        }

        private void OnUseItem(Connection con, UseItem useItem)
        {
            if (_useItemMap[con] == null && _validClasses.Contains(con.Self.Class))
                con.Client.Send(CreateNotification(
                    con.Self.ObjectId, "Auto Ability Activated!"));
            _useItemMap[con] = useItem as UseItem;
        }

        private void OnNewTick(Connection con, NewTick newTick)
        {
            OnUpdate(con, null);
        }

        private void OnUpdate(Connection con, Update update)
        {
            if (_cooldown > 0) _cooldown--;
            if (_cooldown != 0) return;
            if (!_enabled) return;
            if (_useItemMap[con] == null) return;

            float manaPercentage = con.Self[StatType.MP] / (float)con.Self[StatType.MaximumMP];
            float healthPercentage = con.Self[StatType.HP] / (float)con.Self[StatType.MaximumHP];

            switch (con.Self.Class)
            {
                case Classes.Paladin:
                {
                    if (!con.Self.HasEffect(ConditionEffects.Damaging) &&
                        manaPercentage > AutoAbilityConfig.Default.RequiredManaPercent &&
                        AutoAbilityConfig.Default.PaladinAutoBuff)
                    {
                        SendUseItem(con);
                    }
                    break;
                }
                case Classes.Priest:
                {
                    if ((!con.Self.HasEffect(ConditionEffects.Healing) &&
                        manaPercentage > AutoAbilityConfig.Default.RequiredManaPercent &&
                        AutoAbilityConfig.Default.PriestAutoBuff) ||
                        (healthPercentage < AutoAbilityConfig.Default.RequiredHealthPercent &&
                        AutoAbilityConfig.Default.PriestAutoHeal))
                    {
                        SendUseItem(con);
                    }
                    break;
                }
                case Classes.Warrior:
                {
                    if (!con.Self.HasEffect(ConditionEffects.Berserk) &&
                        manaPercentage > AutoAbilityConfig.Default.RequiredManaPercent &&
                        AutoAbilityConfig.Default.WarriorAutoBuff)
                    {
                        SendUseItem(con);
                    }
                    break;
                }
                case Classes.Rogue:
                {
                    if (!con.Self.HasEffect(ConditionEffects.Invisible) &&
                        manaPercentage > AutoAbilityConfig.Default.RequiredManaPercent &&
                        AutoAbilityConfig.Default.RogueAutoCloak)
                    {
                        SendUseItem(con);
                    }
                    break;
                }
            }
        }

        private void OnAACommand(Connection con, string command, string[] args)
        {
            if (args.Length == 0) return;

            if (args[0] == "on")
            {
                _enabled = true;
                con.Client.Send(
                    CreateNotification(
                        con.Self.ObjectId, "Auto Ability Enabled!"));
            }
            else if (args[0] == "off")
            {
                _enabled = false;
                con.Client.Send(
                    CreateNotification(
                        con.Self.ObjectId, "Auto Ability Disabled!"));
            }
            else if (args[0] == "settings")
            {
                ShowGenericSettingsGUI(AutoAbilityConfig.Default, "Auto Ablity Settings");
            }
            else if (args[0] == "test")
            {
                SendUseItem(con);
            }
        }

        private void SendUseItem(Connection con)
        {
            _cooldown = AutoAbilityConfig.Default.RetryDelay;

            UseItem useItem = _useItemMap[con];
            useItem.Time = con.Time;
            useItem.ItemUsePos = con.Self.Status.Position;
            con.Server.Send(useItem);

            if (AutoAbilityConfig.Default.ShowBuffNotifications)
                con.Client.Send(CreateNotification(
                    con.Self.ObjectId, "Auto-Buff Triggered!"));
        }
    }
}
