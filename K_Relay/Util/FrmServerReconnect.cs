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

namespace K_Relay.Util
{
    public partial class FrmServerReconnect : Form
    {
        private ReconnectHandler _r;

        public FrmServerReconnect(ReconnectHandler r)
        {
            InitializeComponent();
            _r = r;

            listServers.SuspendLayout();
            foreach (var pair in Serializer.Servers)
                listServers.Items.Add(pair.Key);

            listServers.ResumeLayout();

        }

        private void listServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = listServers.Items[listServers.SelectedIndex].ToString();
            string server = Serializer.Servers[name];

            _r.ChangeServer(server, name);
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
