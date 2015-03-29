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
        private string _originalHost;
        private int _originalPort;

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
            proxy.HookPacket(PacketType.CREATE_SUCCESS, OnCreateSuccess);

            // So we can restore them later
            _originalHost = _proxy.RemoteAddress;
            _originalPort = _proxy.Port;
        }

        private void OnCreateSuccess(ClientInstance client, Packet createSuccessPacket)
        {
            // Restore the original connection info so new clients can connect normally
            _proxy.RemoteAddress = _originalHost;
            _proxy.Port = _originalPort;
        }

        private void OnReconnectPacket(ClientInstance client, Packet reconnectPacket)
        {
            if (reconnectPacket["Port"] != -1)
            {
                // Next time the proxy gets a client connectiom,
                // The remote connection it sets up will be to here:
                _proxy.Port = reconnectPacket["Port"];
            }

            if (reconnectPacket["Host"] != "")
            {
                // Next time the proxy gets a client connectiom,
                // The remote connection it sets up will be to here:
                _proxy.RemoteAddress = reconnectPacket["Host"];
            }

            // Tell the client to connect to the proxy
            reconnectPacket["Host"] = "localhost";
            reconnectPacket["Port"] = 2050;
        }
    }
}
