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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_Relay.Util
{
    class PacketDebugger : IPlugin
    {
        StreamWriter logWriter;

        private List<PacketType> _reportId = new List<PacketType>()
        {
        };

        private List<PacketType> _printString = new List<PacketType>()
        {
            
        };

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Packet Debugger"; }

        public string GetDescription()
        { return "Helps track what packets have been received."; }

        public string[] GetCommands()
        { return new string[] { }; }

        public void Initialize(Proxy proxy)
        {
            proxy.ClientPacketRecieved += Proxy_ClientPacketRecieved; ;
            proxy.ServerPacketRecieved += Proxy_ServerPacketRecieved; ;
            proxy.ProxyListenStarted += Proxy_ProxyListenStarted;
            proxy.ProxyListenStopped += Proxy_ProxyListenStopped;
        }

        private void Proxy_ServerPacketRecieved(Client client, Packet packet)
        {
            logWriter.WriteLine("Recieved packet from server \n" + packet.ToString());
        }

        private void Proxy_ClientPacketRecieved(Client client, Packet packet)
        {
            logWriter.WriteLine("Sent packet to server \n" + packet.ToString());
        }

        private void Proxy_ProxyListenStarted(Proxy proxy)
        {
            logWriter = new StreamWriter(@"Log" + new Random().Next() + ".txt");
            logWriter.WriteLine("Started logging");
        }

        private void Proxy_ProxyListenStopped(Proxy proxy)
        {
            logWriter.WriteLine("Finished logging");
            logWriter.Close();
        }

      /*  private void OnPacket(Client client, Packet packet)
        {
            if (_reportId.Contains(packet.Type)) Console.WriteLine("[Packet Debugger] Received {0} packet.", packet.Type);
            if (_printString.Contains(packet.Type)) Console.WriteLine("[Packet Debugger] {0}", packet);
            
            

        }*/
    }
}
