using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpamFilter
{
    public partial class FrmSpamFilter : Form
    {
        public FrmSpamFilter()
        {
            InitializeComponent();
            gridConfig.SelectedObject = SpamFilterConfig.Default;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            SpamFilterConfig.Default.Save();
            this.Close();
        }
    }
}
