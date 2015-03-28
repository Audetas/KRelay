using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_Relay
{
    partial class FrmMain
    {
        private void InitLogs()
        {
            btnClearLog.Click += (s, e) => tbxLog.Clear();
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.DefaultExt = ".txt";

            if (s.ShowDialog() == DialogResult.OK)
            {
                FileStream logFile = File.Open(s.FileName, FileMode.Create);
                using (StreamWriter sw = new StreamWriter(logFile))
                { sw.Write(tbxLog.Text); }
                logFile.Close();
            }
        }
    }
}
