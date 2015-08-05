using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;

namespace K_Relay.Util
{
    public partial class FrmServerReconnect : Form
    {
        private ReconnectHandler _r;
        private Client client;
        public FrmServerReconnect(ReconnectHandler r, Client c)
        {
            InitializeComponent();
            _r = r;
            client = c;

            listServers.SuspendLayout();
            foreach (var pair in Serializer.Servers)
                listServers.Items.Add(pair.Key);

            listServers.ResumeLayout();

        }

        private void listServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = listServers.Items[listServers.SelectedIndex].ToString();
            string server = Serializer.Servers[name];

            _r.ChangeServer(server, name, client);
            this.Close();
        }

        private void FrmServerReconnect_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
