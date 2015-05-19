using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Utilities;
using MetroFramework;

namespace K_Relay
{
    partial class FrmMainMetro
    {
        private void InitPackets()
        {
            foreach (PacketType type in Enum.GetValues(typeof (PacketType)).Cast<PacketType>())
                listPackets.ListBox.Items.Insert(0, type.ToString());
        }

        private void btnOpenPacketFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Serializer.DEBUGGetSolutionRoot() + @"\XML\");
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                    MetroMessageBox.Show(this,
                        string.Format(
                            "File not found!\n\nThe directory '{0}' could not be found.\nPlease make sure it exists and Try Again.",
                            Serializer.DEBUGGetSolutionRoot() + @"\XML\"), "Error!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                else
                    MetroMessageBox.Show(this, ex.ToString(), "Error - " + ex.GetType().Name, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }
        
        private void listPackets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPackets.ListBox.SelectedItem != null)
            {
                Type type = Serializer.GetPacketType(
                    (PacketType)Enum.Parse(typeof(PacketType), (string)listPackets.ListBox.SelectedItem));
                tbxPacketInfo.Text = (Activator.CreateInstance(type) as Packet).ToStructure();
            }
        }
    }
}
