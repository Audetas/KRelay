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

        public ConClient(Proxy proxy, Client client)
        {
            _toConnect = client;
            _proxy = proxy;
            currentServ = _proxy.defServer;
            lastRealm = "Nexus";
            Save(currentServ);
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
                    lines[i] = _toConnect.PlayerData.Name + "," + text + "," + lastRealm;
                }
            }
            File.WriteAllLines(path, new string[] { String.Empty });
            File.WriteAllLines(path, lines);
            if (!found)
            {
                using (StreamWriter file = File.AppendText(path))
                {
                    file.WriteLine(_toConnect.PlayerData.Name + "," + text + "," + lastRealm);
                }
            }
        }

        public ReconnectPacket _recon = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
        public ReconnectPacket _drecon = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
    }
}
