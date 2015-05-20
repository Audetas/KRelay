namespace Lib_K_Relay.Utilities
{
    partial class FrmGenericSettings
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
            this.gridSettings = new System.Windows.Forms.PropertyGrid();
            this.menuSettings = new System.Windows.Forms.MenuStrip();
            this.btnDone = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReset = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridSettings
            // 
            this.gridSettings.CategorySplitterColor = System.Drawing.Color.White;
            this.gridSettings.CommandsBackColor = System.Drawing.Color.White;
            this.gridSettings.HelpBackColor = System.Drawing.Color.White;
            this.gridSettings.HelpBorderColor = System.Drawing.Color.Transparent;
            this.gridSettings.HelpVisible = false;
            this.gridSettings.Location = new System.Drawing.Point(0, 22);
            this.gridSettings.Margin = new System.Windows.Forms.Padding(2);
            this.gridSettings.Name = "gridSettings";
            this.gridSettings.Size = new System.Drawing.Size(358, 189);
            this.gridSettings.TabIndex = 0;
            this.gridSettings.ToolbarVisible = false;
            // 
            // menuSettings
            // 
            this.menuSettings.BackColor = System.Drawing.Color.White;
            this.menuSettings.Dock = System.Windows.Forms.DockStyle.None;
            this.menuSettings.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDone,
            this.btnReset});
            this.menuSettings.Location = new System.Drawing.Point(0, 0);
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuSettings.Size = new System.Drawing.Size(154, 24);
            this.menuSettings.TabIndex = 1;
            // 
            // btnDone
            // 
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(47, 22);
            this.btnDone.Text = "Done";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnReset
            // 
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(102, 22);
            this.btnReset.Text = "Reset to Default";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FrmGenericSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 211);
            this.Controls.Add(this.gridSettings);
            this.Controls.Add(this.menuSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuSettings;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FrmGenericSettings";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.Text = "FrmGenericSettings";
            this.TopMost = true;
            this.menuSettings.ResumeLayout(false);
            this.menuSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid gridSettings;
        private System.Windows.Forms.MenuStrip menuSettings;
        private System.Windows.Forms.ToolStripMenuItem btnDone;
        private System.Windows.Forms.ToolStripMenuItem btnReset;
    }
}