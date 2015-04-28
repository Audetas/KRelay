using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapRipper
{
    public partial class HandleForm : MetroForm
    {
        private Plugin m_plugin;
        private FixedStyleManager m_styleManager;

        public HandleForm(Plugin plugin)
        {
            this.m_plugin = plugin;
            InitializeComponent();
            this.m_styleManager = new FixedStyleManager(this);

            this.clrStyle.SelectedValueChanged += clrStyle_SelectedValueChanged;
            this.clrTheme.SelectedValueChanged += clrTheme_SelectedValueChanged;

            this.clrStyle.Items.AddRange(Enum.GetNames(typeof(MetroColorStyle)));
            this.clrTheme.Items.AddRange(Enum.GetNames(typeof(MetroThemeStyle)));

            this.widthLabel.Text = String.Format(this.widthLabel.Text, (this.m_plugin.Map != null ? this.m_plugin.Map.Width.ToString() : "Not in world"));
            this.heightLabel.Text = String.Format(this.heightLabel.Text, (this.m_plugin.Map != null ? this.m_plugin.Map.Height.ToString() : "Not in world"));

            this.metroTile1.Text = (this.m_plugin.Map != null ? this.m_plugin.Map.Name.ToString() : "Not in world");

            this.clrTheme.SelectedItem = this.clrStyle.SelectedItem = "Default";

            if (this.m_plugin.Map != null)
            {
                this.m_plugin.Map.TilesAdded += Map_TilesAdded;
                //this.metroProgressBar1.Maximum = this.m_plugin.Map.Tiles[0].Length * this.m_plugin.Map.Tiles[1].Length;
            }
        }

        void Map_TilesAdded(int currentTiles)
        {
            this.Invoke(new Action(() =>
            {
                //this.metroProgressBar1.Value = currentTiles;
            }));
        }

        private void clrTheme_SelectedValueChanged(object sender, EventArgs e)
        {
            this.m_styleManager.Theme = (MetroThemeStyle)Enum.Parse(typeof(MetroThemeStyle), (string)clrTheme.SelectedItem, true);
        }

        private void clrStyle_SelectedValueChanged(object sender, EventArgs e)
        {
            this.m_styleManager.Style = (MetroColorStyle)Enum.Parse(typeof(MetroColorStyle), (string)clrStyle.SelectedItem, true);
        }

        private void saveMapButton_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "", "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            MetroMessageBox.Show(this, "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MetroMessageBox.Show(this, "", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            MetroMessageBox.Show(this, "", "Hand", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            MetroMessageBox.Show(this, "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MetroMessageBox.Show(this, "", "None", MessageBoxButtons.OK, MessageBoxIcon.None);
            MetroMessageBox.Show(this, "", "Question", MessageBoxButtons.OK, MessageBoxIcon.Question);
            MetroMessageBox.Show(this, "", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            MetroMessageBox.Show(this, "", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private class FixedStyleManager
        {
            private MetroStyleManager m_manager;

            private MetroColorStyle m_colorStyle;
            private MetroThemeStyle m_themeStyle;
            

            public FixedStyleManager(MetroForm form)
            {
                this.m_manager = new MetroStyleManager(form.Container);
                this.m_manager.Owner = form;
            }

            public MetroColorStyle Style
            {
                get { return this.m_colorStyle; }
                set
                {
                    this.m_colorStyle = value;
                    Update();
                }
            }

            public MetroThemeStyle Theme
            {
                get { return this.m_themeStyle; }
                set
                {
                    this.m_themeStyle = value;
                    Update();
                }
            }

            public void Update()
            {
                (this.m_manager.Owner as MetroForm).Theme = this.m_themeStyle;
                (this.m_manager.Owner as MetroForm).Style = this.m_colorStyle;

                this.m_manager.Theme = this.m_themeStyle;
                this.m_manager.Style = this.m_colorStyle;
            }
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.realmeye.com/wiki-search?q=" + this.metroTile1.Text);
        }
    }
}
