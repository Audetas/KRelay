namespace MapRipper
{
    partial class HandleForm
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
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.saveMapButton = new MetroFramework.Controls.MetroButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.heightLabel = new MetroFramework.Controls.MetroLabel();
            this.widthLabel = new MetroFramework.Controls.MetroLabel();
            this.clrTheme = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.clrStyle = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Location = new System.Drawing.Point(150, 30);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(142, 20);
            this.metroTile1.TabIndex = 0;
            this.metroTile1.Text = "Nexus";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTile1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTile1.UseSelectable = true;
            this.metroTile1.Click += new System.EventHandler(this.metroTile1_Click);
            // 
            // saveMapButton
            // 
            this.saveMapButton.Location = new System.Drawing.Point(23, 125);
            this.saveMapButton.Name = "saveMapButton";
            this.saveMapButton.Size = new System.Drawing.Size(253, 33);
            this.saveMapButton.TabIndex = 2;
            this.saveMapButton.Text = "Save Current Map";
            this.saveMapButton.UseSelectable = true;
            this.saveMapButton.Click += new System.EventHandler(this.saveMapButton_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.heightLabel);
            this.metroPanel1.Controls.Add(this.widthLabel);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(24, 63);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(253, 56);
            this.metroPanel1.TabIndex = 3;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(4, 27);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(69, 19);
            this.heightLabel.TabIndex = 3;
            this.heightLabel.Text = "Height: {0}";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(4, 4);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(66, 19);
            this.widthLabel.TabIndex = 2;
            this.widthLabel.Text = "Width: {0}";
            // 
            // clrTheme
            // 
            this.clrTheme.FormattingEnabled = true;
            this.clrTheme.ItemHeight = 23;
            this.clrTheme.Location = new System.Drawing.Point(22, 188);
            this.clrTheme.Name = "clrTheme";
            this.clrTheme.Size = new System.Drawing.Size(254, 29);
            this.clrTheme.TabIndex = 4;
            this.clrTheme.UseSelectable = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(21, 166);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(86, 19);
            this.metroLabel3.TabIndex = 5;
            this.metroLabel3.Text = "Color Theme";
            // 
            // clrStyle
            // 
            this.clrStyle.FormattingEnabled = true;
            this.clrStyle.ItemHeight = 23;
            this.clrStyle.Location = new System.Drawing.Point(21, 242);
            this.clrStyle.Name = "clrStyle";
            this.clrStyle.Size = new System.Drawing.Size(254, 29);
            this.clrStyle.TabIndex = 6;
            this.clrStyle.UseSelectable = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(21, 220);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(73, 19);
            this.metroLabel4.TabIndex = 7;
            this.metroLabel4.Text = "Color Style";
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // HandleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 288);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.clrStyle);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.clrTheme);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.saveMapButton);
            this.Controls.Add(this.metroTile1);
            this.Name = "HandleForm";
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Map Ripper";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroButton saveMapButton;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel heightLabel;
        private MetroFramework.Controls.MetroLabel widthLabel;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroComboBox clrStyle;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox clrTheme;
        private MetroFramework.Components.MetroToolTip metroToolTip1;

    }
}