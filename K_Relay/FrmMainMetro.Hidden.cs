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
        [DllImport("user32.dll")]
        private static extern bool HideCaret(IntPtr hWnd);
        
        private void InitAbout()
        {
            //this.tbxCredits.GotFocus += tbxCredits_GotFocus;
            //this.tbxCredits.MouseDown += tbxCredits_GotFocus;
            //
            //TextBoxAppender.AppendText(tbxCredits.ToWinFormRTB(), " Creator:\n", Color.DodgerBlue, true);
            //TextBoxAppender.AppendText(tbxCredits.ToWinFormRTB(), "  - KrazyShank / Kronks\n\n", Color.Empty, false);
            //TextBoxAppender.AppendText(tbxCredits.ToWinFormRTB(), " Contributors:\n", Color.DodgerBlue, true);
            //TextBoxAppender.AppendText(tbxCredits.ToWinFormRTB(), "  - MrNobody\n", Color.Empty, false);
            //TextBoxAppender.AppendText(tbxCredits.ToWinFormRTB(), "  - Ossimc82\n", Color.Empty, false);
            //TextBoxAppender.AppendText(tbxCredits.ToWinFormRTB(), "  - Knorrex\n", Color.Empty, false);
            //TextBoxAppender.AppendText(tbxCredits.ToWinFormRTB(), "  - Alde\n", Color.Empty, false);
            //TextBoxAppender.AppendText(tbxCredits.ToWinFormRTB(), "  - 059\n\n", Color.Empty, false);
            //TextBoxAppender.AppendText(tbxCredits.ToWinFormRTB(), " Design:\n", Color.DodgerBlue, true);
            //TextBoxAppender.AppendText(tbxCredits.ToWinFormRTB(), "  - Kithio", Color.Empty, false);
        }

        private void tbxCredits_GotFocus(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                //HideCaret(tbxCredits.Handle);
            });
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
