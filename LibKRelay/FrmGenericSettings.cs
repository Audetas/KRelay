using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibKRelay
{
    public partial class FrmGenericSettings : Form
    {
        private dynamic _settings;

        public FrmGenericSettings()
        {
            InitializeComponent();
        }

        public FrmGenericSettings(dynamic settingsObject, string title)
        {
            InitializeComponent();
            _settings = settingsObject;
            Text = title;

            gridSettings.SelectedObject = _settings;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            _settings.Save();
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset the settings to default?", "K Relay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _settings.Reset();
                _settings.Save();
            }
        }
    }
}
