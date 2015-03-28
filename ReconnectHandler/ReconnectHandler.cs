using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconnectHandler
{
    public class ReconnectHandler : IPlugin
    {
        private Proxy _proxy;

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Reconnect Handler"; }

        public string GetDescription()
        { return "Changes the host name to that of the entered portal."; }

        public void Initialize(Proxy proxy)
        {
            _proxy = proxy;
            proxy.HookPacket(PacketType.RECONNECT, OnReconnectPacket);
        }

        private void OnReconnectPacket(ClientInstance client, Packet reconnectPacket)
        {
            if (reconnectPacket.Get<int>("Port") != -1)
            {
                // Next time the proxy gets a client connectiom,
                // The remote connection it sets up will be to here:
                _proxy.Port = reconnectPacket.Get<int>("Port");
            }

            if (reconnectPacket.Get<string>("Host") != "")
            {
                // Next time the proxy gets a client connectiom,
                // The remote connection it sets up will be to here:
                _proxy.ListenAddress = reconnectPacket.Get<string>("Host");
            }

            reconnectPacket["Host"] = "localhost";
            reconnectPacket["Port"] = 2050;
        }
    }
}
