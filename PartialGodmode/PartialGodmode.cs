using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialGodmode
{
    public class PartialGodmode : IPlugin
    {
        private bool _enabled = false;
        private int _numHits = 0;

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Partial Godmode"; }

        public string GetDescription()
        { return "Ignores a specified percentage of projectiles, making them do no damage.\n" +
                 "The more projectiles you ignore, the higher chance you have of disconnecting."; }

        public string[] GetCommands()
        { return new string[] { "/gm on", "/gm off", "/gm settings" }; }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket(PacketType.PLAYERHIT, OnHit);
            proxy.HookPacket(PacketType.SQUAREHIT, OnHit);
            proxy.HookPacket(PacketType.OTHERHIT, OnHit);
            proxy.HookCommand("gm", OnGMCommand);
        }

        private void OnHit(Client client, Packet packet)
        {
            if (!_enabled) return;
            if (packet.Type == PacketType.PLAYERHIT && !PartialGodmodeConfig.Default.BlockPlayerHit) return;
            if (packet.Type == PacketType.SQUAREHIT && !PartialGodmodeConfig.Default.BlockSquareHit) return;
            if (packet.Type == PacketType.OTHERHIT && !PartialGodmodeConfig.Default.BlockOtherHit)   return;

            _numHits++;

            if (_numHits >= PartialGodmodeConfig.Default.NumberOfHitsBeforeBlock)
            {
                _numHits = 0;
                packet.Send = false;
            }
        }

        private void OnGMCommand(Client client, string command, string[] args)
        {
            if (args.Length == 0) return;

            if (args[0] == "on")
            {
                _enabled = true;
                client.SendToClient(
                    PluginUtils.CreateNotification(
                        client.ObjectId, "Partial Godmode Enabled for 1 / " + PartialGodmodeConfig.Default.NumberOfHitsBeforeBlock + " hits!"));
            }
            else if (args[0] == "off")
            {
                _enabled = false;
                client.SendToClient(
                    PluginUtils.CreateNotification(
                        client.ObjectId, "Partial Godmode Disabled!"));
            }
            else if (args[0] == "settings")
            {
                PluginUtils.ShowGenericSettingsGUI(PartialGodmodeConfig.Default, "Partial Godmode Settings");
            }
        }
    }
}
