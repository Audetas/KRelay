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
using Lib_K_Relay.GameData;

namespace K_Relay
{
    partial class FrmMainMetro
    {
        private void InitPackets()
        {
            Invoke((MethodInvoker)delegate
            {
                foreach (PacketType type in Enum.GetValues(typeof(PacketType)).Cast<PacketType>())
                    listPackets.ListBox.Items.Insert(0, type.ToString());
            });
        }
        
        private void listPackets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPackets.ListBox.SelectedItem != null)
            {
				//Type type = GameDataOld.Packets[GameDataOld.PacketTypeMap[
				//    (PacketType)Enum.Parse(typeof(PacketType), (string)listPackets.ListBox.SelectedItem)]].Type;
				Type type = GameData.Packets.ByName((string)listPackets.ListBox.SelectedItem).Type;
                tbxPacketInfo.Text = (Activator.CreateInstance(type) as Packet).ToStructure();
            }
        }
    }
}
