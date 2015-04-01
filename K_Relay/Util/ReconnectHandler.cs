using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace K_Relay.Util
{
    public class ReconnectHandler : IPlugin
    {
        private Proxy _proxy;
        private string _originalHost;
        private int _originalPort;

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Internal Reconnect Handler"; }

        public string GetDescription()
        { return "Changes the host name to that of the entered portal.\n" +
                 "Required to be able to enter portals.\n" + 
                 "You can disable this in the settings if you wish to handle RECONNECT packets yourself."; }

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
            CreateSuccessPacket createSuccess = createSuccessPacket as CreateSuccessPacket;
            // Restore the original connection info so new clients can connect normally
            _proxy.RemoteAddress = _originalHost;
            _proxy.Port = _originalPort;

            // Send welcome message to player
            NotificationPacket notif = (NotificationPacket)Packet.CreateInstance(PacketType.NOTIFICATION);
            notif.ObjectId = createSuccess.ObjectId;
            notif.Message = "{\"key\":\"blank\",\"tokens\":{\"data\":\"Welcome to K Relay\"}}";
            notif.Color = 0x00FFFF;

            Task.Run(() =>
            {
                System.Threading.Thread.Sleep(1000);
                client.SendToClient(notif);
            });
        }

        private void OnReconnectPacket(ClientInstance client, Packet packet)
        {
            ReconnectPacket reconnect = packet as ReconnectPacket;
            if (reconnect.Port != -1)
            {
                _proxy.Port = reconnect.Port;
                Console.WriteLine("[Reconnect Handler] Changed remote port to {0}.", _proxy.Port);
            }

            if (reconnect.Host != "")
            {
                if (reconnect.Host.Contains(".com"))
                    _proxy.RemoteAddress = Dns.GetHostEntry(reconnect.Host).AddressList[0].ToString();
                else
                    _proxy.RemoteAddress = reconnect.Host;
                Console.WriteLine("[Reconnect Handler] Changed remote address to {0}.", _proxy.RemoteAddress);
            }

            // Tell the client to connect to the proxy
            reconnect.Host = "localhost";
            reconnect.Port = 2050;
        }
    }
}
