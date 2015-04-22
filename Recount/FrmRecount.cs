using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recount
{
    public partial class FrmRecount : Form
    {
        private Recount _r;

        public FrmRecount()
        {
            InitializeComponent();
            throw new NotImplementedException("Use the other constructor!");
        }

        public FrmRecount(Recount r)
        {
            InitializeComponent();
            _r = r;

            new Thread(Refresh).Start();
        }

        private void Refresh()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Refresh");

                this.Invoke(new MethodInvoker(() =>
                {
                    lstInfo.SuspendLayout();
                    lstInfo.Items.Clear();
                    foreach (var pair in _r.PlayerNames) lstInfo.Items.Add(new ListViewItem(new string[] { pair.Value, "Class", "DPS", "Total Damage" }));
                    lstInfo.ResumeLayout();
                }));    
            }
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {

        }
    }
}
