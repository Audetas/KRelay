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

namespace Glow
{
    public class Glow : IPlugin
    {
        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Glow"; }

        public string GetDescription()
        { return "You're so excited about K Relay that you're literally glowing!"; }

        public string[] GetCommands()
        { return new string[] { "/AmISpecial" }; }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket(PacketType.UPDATE, OnUpdate);
            proxy.HookCommand("amispecial", OnSpecialCommand);
        }

        private void OnSpecialCommand(Client client, string command, string[] args)
        {
            Random r = new Random();
            int val = 0;
            for (int i = 0; i < 10 ; i++)
            {
                val += r.Next(400000, 723411);
                client.SendToClient(PluginUtils.CreateNotification(
                    client.ObjectId, val, "YOU ARE SPECIAL!"));
            }
        }

        private void OnUpdate(Client client, Packet packet)
        {
            UpdatePacket update = (UpdatePacket)packet;

            for (int i = 0; i < update.NewObjs.Length; i++)
            {
                if (update.NewObjs[i].Status.ObjectId == client.ObjectId)
                {
                    for (int j = 0; j < update.NewObjs[i].Status.Data.Length; j++)
                    {
                        if (update.NewObjs[i].Status.Data[j].Id == 59)
                            update.NewObjs[i].Status.Data[j].IntValue = 100;
                    }
                }
            }
        }
    }
}
