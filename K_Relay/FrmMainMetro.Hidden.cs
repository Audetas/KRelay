using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace K_Relay
{
    partial class FrmMainMetro
    {
        private void tglStartByDefault_CheckedChanged(object sender, EventArgs e)
        {
            lblStartByDefault.Text = tglStartByDefault.Checked.ToString();
        }

        private void tglUseInternalReconnectHandler_CheckedChanged(object sender, EventArgs e)
        {
            lblUseInternalReconnectHandler.Text = tglUseInternalReconnectHandler.Checked.ToString();
        }

        private void lblUseInternalReconnectHandler_Click(object sender, EventArgs e)
        {
            tglUseInternalReconnectHandler.Checked = !tglUseInternalReconnectHandler.Checked;
        }

        private void lblStartByDefault_Click(object sender, EventArgs e)
        {
            tglStartByDefault.Checked = !tglStartByDefault.Checked;
        }
    }
}
