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

namespace FameNotifier
{
    public class FameNotifier : IPlugin
    {
        private Dictionary<Client, int> _fame = new Dictionary<Client, int>();

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Fame Notifer"; }

        public string GetDescription()
        { return "Lets you know when your fame has increased."; }

        public string[] GetCommands()
        { return new string[] { }; }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket(PacketType.UPDATE, OnUpdate);
            proxy.HookPacket(PacketType.NEWTICK, OnUpdate);
            proxy.ClientConnected += (client) => _fame.Add(client, -1);
            proxy.ClientDisconnected += (client) => _fame.Remove(client);
        }

        private void OnUpdate(Client client, Packet packet)
        {
            int fame = _fame[client];
            _fame[client] = client.PlayerData.CharacterFame;

            if (fame != -1 && client.PlayerData.CharacterFame != fame)
                client.SendToClient(
                    PluginUtils.CreateNotification(
                        client.ObjectId, "+" + (client.PlayerData.CharacterFame - fame) + " fame!"));
        }
    }
}
