using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace K_Relay.Util
{
    public class ConClient
    {
        public Proxy _proxy;
        public Client _toConnect;
        public string currentServ = " ";
        public string lastRealm = " ";
        public string lastRealmIP = " ";
        public string lastRealmTime = " ";
        public bool hasWelcomed = false;
        public bool reconnectReady = false;
        public string[] reconnectData;

        public ReconnectPacket _recon = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
        public ReconnectPacket _drecon = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
        public ReconnectPacket _ghall = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
        public ReconnectPacket _yard = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
        public ReconnectPacket _vault = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);

        public ConClient(Proxy proxy, Client client)
        {
            _toConnect = client;
            _proxy = proxy;
            currentServ = _proxy.defServer;
            lastRealm = "Nexus";
            Save(currentServ);

            _vault.IsFromArena = false;
            _vault.GameId = -5;
            _vault.Host = "localhost"; // (reconnect.Host == "" ? _proxy.getRemoteAddress(client) : reconnect.Host);
            _vault.Port = 2050;
            _vault.Key = new byte[0];
            _vault.KeyTime = 0;
            _vault.Name = "{\"text\":\"server.vault\"}";
            _vault.Send = true;

            _yard.IsFromArena = false;
            _yard.GameId = 3235;
            _yard.Host = "localhost"; // (reconnect.Host == "" ? _proxy.getRemoteAddress(client) : reconnect.Host);
            _yard.Port = 2050;
            _yard.Key = new byte[0];
            _yard.KeyTime = 0;
            _yard.Name = "Pet Yard";
            _yard.Send = true;

            _ghall.IsFromArena = false;
            _ghall.GameId = 3244;
            _ghall.Host = "localhost"; // (reconnect.Host == "" ? _proxy.getRemoteAddress(client) : reconnect.Host);
            _ghall.Port = 2050;
            _ghall.Key = new byte[0];
            _ghall.KeyTime = 0;
            _ghall.Name = "Guild Hall";
            _ghall.Send = true;
        }

        public void setCurrentServer(string serv)
        {
            currentServ = serv;

            Save(currentServ);
        }

        public void setRealm(string realm)
        {
            lastRealm = realm;

            Save(currentServ);
        }

        public void Save(string text)
        {
            if (_toConnect.PlayerData.Name == "") return;

            string path = Directory.GetCurrentDirectory();
            path += "\\Plugins\\";
            Directory.CreateDirectory(path);
            path += "CurrentServers.txt";
            if (!File.Exists(path))
            {
                File.WriteAllLines(path, new string[] { String.Empty });
            }
            string[] lines = File.ReadAllLines(path);
            bool found = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] values = line.Split(',');
                if (values[0] == _toConnect.PlayerData.Name)
                {
                    found = true;
                    lines[i] = _toConnect.PlayerData.Name + "," + text + "," + lastRealm + "," + lastRealmIP + "," + lastRealmTime;
                }
            }
            File.WriteAllLines(path, new string[] { String.Empty });
            File.WriteAllLines(path, lines);
            if (!found)
            {
                using (StreamWriter file = File.AppendText(path))
                {
                    file.WriteLine(_toConnect.PlayerData.Name + "," + text + "," + lastRealm + "," + lastRealmIP + "," + lastRealmTime);
                }
            }
        }

    }
}
