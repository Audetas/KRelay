using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapFilter
{
    public partial class FrmMapFilterSettings : Form
    {
        MapFilter _m;

        public FrmMapFilterSettings(MapFilter m)
        {
            _m = m;
            InitializeComponent();
            foreach (string filter in MapFilterConfig.Default.TileFilters)
                listTileFilters.Items.Add(filter);
            foreach (string filter in MapFilterConfig.Default.ObjectFilters)
                listObjectFilters.Items.Add(filter);
            chkEnabled.Checked = MapFilterConfig.Default.Enabled;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            MapFilterConfig.Default.Save();
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset your settings?", "Map Filter", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MapFilterConfig.Default.Reset();
                MapFilterConfig.Default.Save();
                this.Close();
            }
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            MapFilterConfig.Default.Enabled = chkEnabled.Checked;
        }

        private void btnAddTile_Click(object sender, EventArgs e)
        {
            string tile = "";
            string replacement = "";
            new FrmEnumerator(Serializer.Tiles, "Choose the tile to replace...", (s) => tile = s).ShowDialog();
            new FrmEnumerator(Serializer.Tiles, "Choose the replacement tile...", (s) => replacement = s).ShowDialog();

            if (tile != "" && replacement != "")
            {
                string filter = tile + "=>" + replacement;
                listTileFilters.Items.Add(filter);
                MapFilterConfig.Default.TileFilters.Add(filter);
            }
        }

        private void btnRemoveTile_Click(object sender, EventArgs e)
        {
            if (listTileFilters.SelectedItem == null) return;
            MapFilterConfig.Default.TileFilters.Remove(listTileFilters.SelectedItem.ToString());
            listTileFilters.Items.Remove(listTileFilters.SelectedItem);
        }

        private void btnAddObject_Click(object sender, EventArgs e)
        {
            string obj = "";
            string replacement = "";
            new FrmEnumerator(Serializer.Objects, "Choose the object to replace...", (s) => obj = s).ShowDialog();
            new FrmEnumerator(Serializer.Objects, "Choose the replacement object...", (s) => replacement = s).ShowDialog();

            if (obj != "" && replacement != "")
            {
                string filter = obj + "=>" + replacement;
                listObjectFilters.Items.Add(filter);
                MapFilterConfig.Default.ObjectFilters.Add(filter);
            }
        }

        private void btnRemoveObject_Click(object sender, EventArgs e)
        {
            if (listObjectFilters.SelectedItem == null) return;
            MapFilterConfig.Default.ObjectFilters.Remove(listObjectFilters.SelectedItem.ToString());
            listObjectFilters.Items.Remove(listObjectFilters.SelectedItem);
        }

        private void FrmMapFilterSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            _m.RebuildCache();
        }
    }
}
