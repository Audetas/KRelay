using Lib_K_Relay.GameData;
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

namespace WorldEdit
{
    public partial class FrmWorldEdit : Form
    {
        WorldEdit _w;

        public FrmWorldEdit(WorldEdit w)
        {
            InitializeComponent();
            _w = w;

            listTiles.SuspendLayout();
            foreach (var pair in GameData.Tiles.Map) listTiles.Items.Add(pair.Value.Name);
            listTiles.ResumeLayout();
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            listTiles.SuspendLayout();
            listTiles.Items.Clear();
            foreach (var pair in GameData.Tiles.Map)
                if (pair.Value.Name.ToLower().Contains(tbxSearch.Text.ToLower()))
                    listTiles.Items.Add(pair.Value.Name);
            listTiles.ResumeLayout();
        }

        private void listTiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTiles.SelectedItems.Count == 0) return;

            string selected = listTiles.Items[listTiles.SelectedItems[0].Index].Text;
            lblSelected.Text = "Selected Tile: " + selected;
            _w.SelectedTile = selected;
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (_w.Editing)
            {
                _w.Editing = false;
                btnToggle.Text = "Start Painting";
            }
            else
            {
                _w.Editing = true;
                btnToggle.Text = "Stop Painting";
            }
        }

        private void FrmWorldEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            _w.Editing = false;
        }
    }
}
