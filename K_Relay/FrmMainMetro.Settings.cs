using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib_K_Relay.Utilities;
using MetroFramework;

namespace K_Relay
{
    partial class FrmMainMetro
    {
        private void InitSettings()
        {
            tglStartByDefault.Checked = Config.Default.StartProxyByDefault;
            tglUseInternalReconnectHandler.Checked = Config.Default.UseInternalReconnectHandler;
            lstServers.Items.AddRange(Serializer.Servers.Keys.ToArray());
            lstServers.SelectedItem = Config.Default.DefaultServerName;
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            Config.Default.StartProxyByDefault = tglStartByDefault.Checked;
            Config.Default.UseInternalReconnectHandler = tglUseInternalReconnectHandler.Checked;
            Config.Default.DefaultServerName = lstServers.SelectedItem.ToString();
            Config.Default.Save();

            MetroMessageBox.Show(this, "\nYour settings have been saved.", "Save Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
