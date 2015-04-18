using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_Relay
{
    partial class FrmMain
    {
        private void InitSettings()
        {
            chkStartByDefault.Checked = Config.Default.StartProxyByDefault;
            chkUseInternalReconnectHandler.Checked = Config.Default.UseInternalReconnectHandler;
            lstServers.Items.AddRange(Serializer.Servers.Keys.ToArray());
            lstServers.SelectedItem = Config.Default.DefaultServerName;
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            Config.Default.StartProxyByDefault = chkStartByDefault.Checked;
            Config.Default.UseInternalReconnectHandler = chkUseInternalReconnectHandler.Checked;
            Config.Default.DefaultServerName = lstServers.SelectedItem.ToString();
            Config.Default.Save();
        }
    }
}
