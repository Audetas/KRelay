namespace MapFilter
{
    partial class FrmMapFilterSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabTiles = new System.Windows.Forms.TabPage();
            this.tabObjects = new System.Windows.Forms.TabPage();
            this.menuSettings = new System.Windows.Forms.MenuStrip();
            this.btnDone = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReset = new System.Windows.Forms.ToolStripMenuItem();
            this.listTileFilters = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddTile = new System.Windows.Forms.Button();
            this.btnRemoveTile = new System.Windows.Forms.Button();
            this.btnRemoveObject = new System.Windows.Forms.Button();
            this.btnAddObject = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listObjectFilters = new System.Windows.Forms.ListBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.tabMain.SuspendLayout();
            this.tabTiles.SuspendLayout();
            this.tabObjects.SuspendLayout();
            this.menuSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabTiles);
            this.tabMain.Controls.Add(this.tabObjects);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 40);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(560, 701);
            this.tabMain.TabIndex = 0;
            // 
            // tabTiles
            // 
            this.tabTiles.Controls.Add(this.btnRemoveTile);
            this.tabTiles.Controls.Add(this.btnAddTile);
            this.tabTiles.Controls.Add(this.label1);
            this.tabTiles.Controls.Add(this.listTileFilters);
            this.tabTiles.Location = new System.Drawing.Point(4, 34);
            this.tabTiles.Name = "tabTiles";
            this.tabTiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabTiles.Size = new System.Drawing.Size(552, 663);
            this.tabTiles.TabIndex = 0;
            this.tabTiles.Text = "Tiles (Ground)";
            this.tabTiles.UseVisualStyleBackColor = true;
            // 
            // tabObjects
            // 
            this.tabObjects.Controls.Add(this.btnRemoveObject);
            this.tabObjects.Controls.Add(this.btnAddObject);
            this.tabObjects.Controls.Add(this.label2);
            this.tabObjects.Controls.Add(this.listObjectFilters);
            this.tabObjects.Location = new System.Drawing.Point(4, 34);
            this.tabObjects.Name = "tabObjects";
            this.tabObjects.Padding = new System.Windows.Forms.Padding(3);
            this.tabObjects.Size = new System.Drawing.Size(552, 663);
            this.tabObjects.TabIndex = 1;
            this.tabObjects.Text = "Objects (Colliders)";
            this.tabObjects.UseVisualStyleBackColor = true;
            // 
            // menuSettings
            // 
            this.menuSettings.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDone,
            this.btnReset});
            this.menuSettings.Location = new System.Drawing.Point(0, 0);
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(560, 40);
            this.menuSettings.TabIndex = 1;
            this.menuSettings.Text = "menuStrip1";
            // 
            // btnDone
            // 
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(85, 38);
            this.btnDone.Text = "Done";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnReset
            // 
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(209, 38);
            this.btnReset.Text = "Reset to Defaults";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // listTileFilters
            // 
            this.listTileFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listTileFilters.FormattingEnabled = true;
            this.listTileFilters.ItemHeight = 25;
            this.listTileFilters.Location = new System.Drawing.Point(6, 35);
            this.listTileFilters.Name = "listTileFilters";
            this.listTileFilters.Size = new System.Drawing.Size(540, 554);
            this.listTileFilters.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tile Filters: (Original=>ReplaceWith)";
            // 
            // btnAddTile
            // 
            this.btnAddTile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddTile.Location = new System.Drawing.Point(6, 595);
            this.btnAddTile.Name = "btnAddTile";
            this.btnAddTile.Size = new System.Drawing.Size(60, 60);
            this.btnAddTile.TabIndex = 2;
            this.btnAddTile.Text = "+";
            this.btnAddTile.UseVisualStyleBackColor = true;
            this.btnAddTile.Click += new System.EventHandler(this.btnAddTile_Click);
            // 
            // btnRemoveTile
            // 
            this.btnRemoveTile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveTile.Location = new System.Drawing.Point(484, 595);
            this.btnRemoveTile.Name = "btnRemoveTile";
            this.btnRemoveTile.Size = new System.Drawing.Size(60, 60);
            this.btnRemoveTile.TabIndex = 3;
            this.btnRemoveTile.Text = "-";
            this.btnRemoveTile.UseVisualStyleBackColor = true;
            this.btnRemoveTile.Click += new System.EventHandler(this.btnRemoveTile_Click);
            // 
            // btnRemoveObject
            // 
            this.btnRemoveObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveObject.Location = new System.Drawing.Point(484, 595);
            this.btnRemoveObject.Name = "btnRemoveObject";
            this.btnRemoveObject.Size = new System.Drawing.Size(60, 60);
            this.btnRemoveObject.TabIndex = 7;
            this.btnRemoveObject.Text = "-";
            this.btnRemoveObject.UseVisualStyleBackColor = true;
            this.btnRemoveObject.Click += new System.EventHandler(this.btnRemoveObject_Click);
            // 
            // btnAddObject
            // 
            this.btnAddObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddObject.Location = new System.Drawing.Point(6, 595);
            this.btnAddObject.Name = "btnAddObject";
            this.btnAddObject.Size = new System.Drawing.Size(60, 60);
            this.btnAddObject.TabIndex = 6;
            this.btnAddObject.Text = "+";
            this.btnAddObject.UseVisualStyleBackColor = true;
            this.btnAddObject.Click += new System.EventHandler(this.btnAddObject_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(385, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Object Filters: (Original=>ReplaceWith)";
            // 
            // listObjectFilters
            // 
            this.listObjectFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listObjectFilters.FormattingEnabled = true;
            this.listObjectFilters.ItemHeight = 25;
            this.listObjectFilters.Location = new System.Drawing.Point(6, 35);
            this.listObjectFilters.Name = "listObjectFilters";
            this.listObjectFilters.Size = new System.Drawing.Size(540, 554);
            this.listObjectFilters.TabIndex = 4;
            // 
            // chkEnabled
            // 
            this.chkEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(346, 44);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(207, 29);
            this.chkEnabled.TabIndex = 2;
            this.chkEnabled.Text = "Enable MapFilter";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
            // 
            // FrmMapFilterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 741);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.menuSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuSettings;
            this.Name = "FrmMapFilterSettings";
            this.Text = "Map Filter Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMapFilterSettings_FormClosing);
            this.tabMain.ResumeLayout(false);
            this.tabTiles.ResumeLayout(false);
            this.tabTiles.PerformLayout();
            this.tabObjects.ResumeLayout(false);
            this.tabObjects.PerformLayout();
            this.menuSettings.ResumeLayout(false);
            this.menuSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabTiles;
        private System.Windows.Forms.TabPage tabObjects;
        private System.Windows.Forms.MenuStrip menuSettings;
        private System.Windows.Forms.ToolStripMenuItem btnDone;
        private System.Windows.Forms.ToolStripMenuItem btnReset;
        private System.Windows.Forms.ListBox listTileFilters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddTile;
        private System.Windows.Forms.Button btnRemoveTile;
        private System.Windows.Forms.Button btnRemoveObject;
        private System.Windows.Forms.Button btnAddObject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listObjectFilters;
        private System.Windows.Forms.CheckBox chkEnabled;

    }
}