using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking
{
    public class State
    {
        public Client Client;
        public string GUID;
        public string ACCID;

        public byte[] ConRealKey = new byte[0];
        public string ConTargetAddress = Proxy.DefaultServer;
        public int ConTargetPort = 2050;

        public ReconnectPacket LastRealm = null;
        public ReconnectPacket LastDungeon = null;

        public State(Client client, string id)
        {
            Client = client;
            GUID = id;
        }
    }
}
