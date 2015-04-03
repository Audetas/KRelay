using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            foreach (PacketType type in Enum.GetValues(typeof(PacketType)).Cast<PacketType>())
                listPackets.Items.Insert(0, type.ToString());

            //treePackets
        }

        private void treePackets_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Type type = Serializer.GetPacketType(
                (PacketType)Enum.Parse(typeof(PacketType), e.Node.Text));
            tbxPacketInfo.Text = (Activator.CreateInstance(type) as Packet).ToStructure();
        }

        private void btnOpenPacketsFolder_Click(object sender, EventArgs e)
        {
            try { Process.Start(Serializer.DEBUGGetSolutionRoot() + @"\XML\"); }
            catch (FileNotFoundException) { Console.WriteLine("[Packet Manager] Uh Oh, directory '{0}' not found.", Serializer.DEBUGGetSolutionRoot() + @"\XML\"); }
        }

        protected void listPackets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPackets.SelectedItem != null)
            {
                Type type = Serializer.GetPacketType(
                (PacketType)Enum.Parse(typeof(PacketType), (string)listPackets.SelectedItem));
                tbxPacketInfo.Text = (Activator.CreateInstance(type) as Packet).ToStructure();
            }
        }
    }
}
