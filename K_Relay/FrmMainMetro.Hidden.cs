using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace K_Relay
{
    partial class FrmMainMetro
    {
        private void InitAbout()
        {
            AppendText(tbxCredits, " Creator:\n", Color.DodgerBlue, true);
            AppendText(tbxCredits, "  - KrazyShank / Kronks\n\n", Color.Black, false);
            AppendText(tbxCredits, " Contributors:\n", Color.DodgerBlue, true);
            AppendText(tbxCredits, "  - MrNobody\n", Color.Black, false);
            AppendText(tbxCredits, "  - Ossimc82\n", Color.Black, false);
            AppendText(tbxCredits, "  - Knorrex\n", Color.Black, false);
            AppendText(tbxCredits, "  - Alde\n", Color.Black, false);
            AppendText(tbxCredits, "  - 059\n\n", Color.Black, false);
            AppendText(tbxCredits, " Design:\n", Color.DodgerBlue, true);
            AppendText(tbxCredits, "  - Kithio", Color.Black, false);
        }

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
