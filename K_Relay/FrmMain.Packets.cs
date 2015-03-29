using Lib_K_Relay.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_Relay
{
    partial class FrmMain
    {
        private void InitPackets()
        {
            Console.WriteLine("[Packet Serializer] Looking for packets in {0}", Config.Default.PacketDirectory);

            PacketSerializer.SerializePacketsFromXmls(
                Config.Default.PacketDirectory.Replace("%startuppath%", Application.StartupPath) + @"\PacketDefinitions.xml",
                Config.Default.PacketDirectory.Replace("%startuppath%", Application.StartupPath) + @"\PacketIDs.xml");

            foreach (PacketType type in Enum.GetValues(typeof(PacketType)).Cast<PacketType>())
            {
                if (PacketSerializer.GetStructure(type).Type != PacketType.UNKNOWN)
                    treePackets.Nodes.Insert(0, type.ToString());
                else
                    treePackets.Nodes.Add("[Unknown] " + type.ToString());
            }

            treePackets.Sort();
        }

        private void treePackets_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tbxPacketInfo.Text = PacketSerializer.GetStructure(
                (PacketType)Enum.Parse(typeof(PacketType),
                e.Node.Text.Replace("[Unknown] ", ""))).ToString();
        }

        private void btnOpenPacketsFolder_Click(object sender, EventArgs e)
        {
            Process.Start(
                Config.Default.PacketDirectory.ToLower().Replace(
                    "%startuppath%", Application.StartupPath));
        }
    }
}
