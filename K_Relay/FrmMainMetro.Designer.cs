namespace K_Relay
{
    partial class FrmMainMetro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainMetro));
            this.tabMain = new MetroFramework.Controls.MetroTabControl();
            this.tabInfo = new MetroFramework.Controls.MetroTabPage();
            this.tbxLog = new MetroFramework.Controls.MetroTextBox();
            this.menuInfo = new System.Windows.Forms.MenuStrip();
            this.btnToggleProxy = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSaveLog = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClearLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPlugins = new MetroFramework.Controls.MetroTabPage();
            this.menuPlugins = new System.Windows.Forms.MenuStrip();
            this.openPluginFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbxPluginInfo = new K_Relay.Controls.MetroRichTextBox();
            this.listPlugins = new K_Relay.Controls.MetroListBox();
            this.tabPackets = new MetroFramework.Controls.MetroTabPage();
            this.menuPackets = new System.Windows.Forms.MenuStrip();
            this.openPacketFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbxPacketInfo = new K_Relay.Controls.MetroRichTextBox();
            this.listPackets = new K_Relay.Controls.MetroListBox();
            this.tabSettings = new MetroFramework.Controls.MetroTabPage();
            this.lblStartByDefault = new MetroFramework.Controls.MetroLabel();
            this.tglStartByDefault = new MetroFramework.Controls.MetroToggle();
            this.lblSBD = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.lblInfo = new MetroFramework.Controls.MetroLabel();
            this.styleCombobox = new MetroFramework.Controls.MetroComboBox();
            this.styleLabel = new MetroFramework.Controls.MetroLabel();
            this.themeCombobox = new MetroFramework.Controls.MetroComboBox();
            this.themeLabel = new MetroFramework.Controls.MetroLabel();
            this.lstServers = new MetroFramework.Controls.MetroComboBox();
            this.lblDefaultServer = new MetroFramework.Controls.MetroLabel();
            this.menuSettings = new System.Windows.Forms.MenuStrip();
            this.btnSaveSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tabAbout = new MetroFramework.Controls.MetroTabPage();
            this.pnlSide = new System.Windows.Forms.Panel();
            this.pbxGang = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.lblVersion = new MetroFramework.Controls.MetroLabel();
            this.lblStatus = new MetroFramework.Controls.MetroLabel();
            this.tabMain.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.menuInfo.SuspendLayout();
            this.tabPlugins.SuspendLayout();
            this.menuPlugins.SuspendLayout();
            this.tabPackets.SuspendLayout();
            this.menuPackets.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.menuSettings.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.pnlSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGang)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabInfo);
            this.tabMain.Controls.Add(this.tabPlugins);
            this.tabMain.Controls.Add(this.tabPackets);
            this.tabMain.Controls.Add(this.tabSettings);
            this.tabMain.Controls.Add(this.tabAbout);
            this.tabMain.Location = new System.Drawing.Point(32, 82);
            this.tabMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(834, 471);
            this.tabMain.TabIndex = 0;
            this.tabMain.UseSelectable = true;
            this.tabMain.UseStyleColors = true;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.tbxLog);
            this.tabInfo.Controls.Add(this.menuInfo);
            this.tabInfo.HorizontalScrollbarBarColor = false;
            this.tabInfo.HorizontalScrollbarHighlightOnWheel = false;
            this.tabInfo.HorizontalScrollbarSize = 0;
            this.tabInfo.Location = new System.Drawing.Point(4, 38);
            this.tabInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Size = new System.Drawing.Size(826, 429);
            this.tabInfo.TabIndex = 0;
            this.tabInfo.Text = "Information";
            this.tabInfo.VerticalScrollbarBarColor = false;
            this.tabInfo.VerticalScrollbarHighlightOnWheel = false;
            this.tabInfo.VerticalScrollbarSize = 0;
            // 
            // tbxLog
            // 
            this.tbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxLog.Lines = new string[0];
            this.tbxLog.Location = new System.Drawing.Point(0, 51);
            this.tbxLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxLog.MaxLength = 2147483647;
            this.tbxLog.Multiline = true;
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.PasswordChar = '\0';
            this.tbxLog.ReadOnly = true;
            this.tbxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxLog.SelectedText = "";
            this.tbxLog.Size = new System.Drawing.Size(826, 378);
            this.tbxLog.TabIndex = 0;
            this.tbxLog.UseSelectable = true;
            this.tbxLog.UseStyleColors = true;
            // 
            // menuInfo
            // 
            this.menuInfo.AutoSize = false;
            this.menuInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(113)))), ((int)(((byte)(189)))));
            this.menuInfo.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnToggleProxy,
            this.btnSaveLog,
            this.btnClearLog});
            this.menuInfo.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuInfo.Location = new System.Drawing.Point(0, 0);
            this.menuInfo.Name = "menuInfo";
            this.menuInfo.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuInfo.Size = new System.Drawing.Size(826, 51);
            this.menuInfo.TabIndex = 0;
            this.menuInfo.Text = "Info";
            // 
            // btnToggleProxy
            // 
            this.btnToggleProxy.Enabled = false;
            this.btnToggleProxy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleProxy.Name = "btnToggleProxy";
            this.btnToggleProxy.Size = new System.Drawing.Size(154, 45);
            this.btnToggleProxy.Text = "Start Proxy";
            this.btnToggleProxy.Click += new System.EventHandler(this.btnToggleProxy_Click);
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(96, 45);
            this.btnSaveLog.Text = "Save Log";
            this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(98, 45);
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // tabPlugins
            // 
            this.tabPlugins.Controls.Add(this.menuPlugins);
            this.tabPlugins.Controls.Add(this.tbxPluginInfo);
            this.tabPlugins.Controls.Add(this.listPlugins);
            this.tabPlugins.HorizontalScrollbarBarColor = false;
            this.tabPlugins.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPlugins.HorizontalScrollbarSize = 0;
            this.tabPlugins.Location = new System.Drawing.Point(4, 38);
            this.tabPlugins.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPlugins.Name = "tabPlugins";
            this.tabPlugins.Size = new System.Drawing.Size(826, 429);
            this.tabPlugins.TabIndex = 0;
            this.tabPlugins.Text = "Plugins";
            this.tabPlugins.VerticalScrollbarBarColor = false;
            this.tabPlugins.VerticalScrollbarHighlightOnWheel = false;
            this.tabPlugins.VerticalScrollbarSize = 0;
            // 
            // menuPlugins
            // 
            this.menuPlugins.AutoSize = false;
            this.menuPlugins.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(113)))), ((int)(((byte)(189)))));
            this.menuPlugins.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuPlugins.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPluginFolderToolStripMenuItem});
            this.menuPlugins.Location = new System.Drawing.Point(300, 0);
            this.menuPlugins.Name = "menuPlugins";
            this.menuPlugins.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuPlugins.Size = new System.Drawing.Size(526, 51);
            this.menuPlugins.TabIndex = 0;
            this.menuPlugins.Text = "menuStrip1";
            // 
            // openPluginFolderToolStripMenuItem
            // 
            this.openPluginFolderToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.openPluginFolderToolStripMenuItem.Name = "openPluginFolderToolStripMenuItem";
            this.openPluginFolderToolStripMenuItem.Size = new System.Drawing.Size(187, 45);
            this.openPluginFolderToolStripMenuItem.Text = "Open Plugin Folder";
            this.openPluginFolderToolStripMenuItem.Click += new System.EventHandler(this.btnOpenPluginFolder_Click);
            // 
            // tbxPluginInfo
            // 
            this.tbxPluginInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tbxPluginInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxPluginInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbxPluginInfo.Lines = new string[] {
        "Select a Plugin to view its description."};
            this.tbxPluginInfo.Location = new System.Drawing.Point(298, 51);
            this.tbxPluginInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxPluginInfo.MaxLength = 2147483647;
            this.tbxPluginInfo.Multiline = true;
            this.tbxPluginInfo.Name = "tbxPluginInfo";
            this.tbxPluginInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.tbxPluginInfo.SelectedText = "";
            this.tbxPluginInfo.Size = new System.Drawing.Size(524, 355);
            this.tbxPluginInfo.TabIndex = 0;
            this.tbxPluginInfo.Text = "Select a Plugin to view its description.";
            this.tbxPluginInfo.UseSelectable = true;
            this.tbxPluginInfo.UseStyleColors = true;
            // 
            // listPlugins
            // 
            this.listPlugins.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listPlugins.Dock = System.Windows.Forms.DockStyle.Left;
            this.listPlugins.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPlugins.FormattingEnabled = true;
            this.listPlugins.IntegralHeight = false;
            this.listPlugins.ItemHeight = 15;
            this.listPlugins.Location = new System.Drawing.Point(0, 0);
            this.listPlugins.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listPlugins.Name = "listPlugins";
            this.listPlugins.Size = new System.Drawing.Size(300, 429);
            this.listPlugins.TabIndex = 0;
            this.listPlugins.TabStop = false;
            this.listPlugins.UseSelectable = true;
            this.listPlugins.UseStyleColors = true;
            this.listPlugins.SelectedIndexChanged += new System.EventHandler(this.listPlugins_SelectedIndexChanged);
            // 
            // tabPackets
            // 
            this.tabPackets.Controls.Add(this.menuPackets);
            this.tabPackets.Controls.Add(this.tbxPacketInfo);
            this.tabPackets.Controls.Add(this.listPackets);
            this.tabPackets.HorizontalScrollbarBarColor = false;
            this.tabPackets.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPackets.HorizontalScrollbarSize = 0;
            this.tabPackets.Location = new System.Drawing.Point(4, 38);
            this.tabPackets.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPackets.Name = "tabPackets";
            this.tabPackets.Size = new System.Drawing.Size(826, 429);
            this.tabPackets.TabIndex = 0;
            this.tabPackets.Text = "Packets";
            this.tabPackets.VerticalScrollbarBarColor = false;
            this.tabPackets.VerticalScrollbarHighlightOnWheel = false;
            this.tabPackets.VerticalScrollbarSize = 0;
            // 
            // menuPackets
            // 
            this.menuPackets.AutoSize = false;
            this.menuPackets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(113)))), ((int)(((byte)(189)))));
            this.menuPackets.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuPackets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPacketFolderToolStripMenuItem});
            this.menuPackets.Location = new System.Drawing.Point(300, 0);
            this.menuPackets.Name = "menuPackets";
            this.menuPackets.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuPackets.Size = new System.Drawing.Size(526, 51);
            this.menuPackets.TabIndex = 0;
            this.menuPackets.Text = "menuStrip1";
            // 
            // openPacketFolderToolStripMenuItem
            // 
            this.openPacketFolderToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.openPacketFolderToolStripMenuItem.Name = "openPacketFolderToolStripMenuItem";
            this.openPacketFolderToolStripMenuItem.Size = new System.Drawing.Size(190, 45);
            this.openPacketFolderToolStripMenuItem.Text = "Open Packet Folder";
            // 
            // tbxPacketInfo
            // 
            this.tbxPacketInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tbxPacketInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxPacketInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPacketInfo.Lines = new string[] {
        "Select a Packet to view its description."};
            this.tbxPacketInfo.Location = new System.Drawing.Point(298, 51);
            this.tbxPacketInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxPacketInfo.MaxLength = 2147483647;
            this.tbxPacketInfo.Multiline = true;
            this.tbxPacketInfo.Name = "tbxPacketInfo";
            this.tbxPacketInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.tbxPacketInfo.SelectedText = "";
            this.tbxPacketInfo.Size = new System.Drawing.Size(524, 355);
            this.tbxPacketInfo.TabIndex = 0;
            this.tbxPacketInfo.Text = "Select a Packet to view its description.";
            this.tbxPacketInfo.UseSelectable = true;
            this.tbxPacketInfo.UseStyleColors = true;
            // 
            // listPackets
            // 
            this.listPackets.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listPackets.Dock = System.Windows.Forms.DockStyle.Left;
            this.listPackets.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPackets.FormattingEnabled = true;
            this.listPackets.IntegralHeight = false;
            this.listPackets.ItemHeight = 15;
            this.listPackets.Location = new System.Drawing.Point(0, 0);
            this.listPackets.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listPackets.Name = "listPackets";
            this.listPackets.Size = new System.Drawing.Size(300, 429);
            this.listPackets.TabIndex = 0;
            this.listPackets.TabStop = false;
            this.listPackets.UseSelectable = true;
            this.listPackets.UseStyleColors = true;
            this.listPackets.SelectedIndexChanged += new System.EventHandler(this.listPackets_SelectedIndexChanged);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.lblStartByDefault);
            this.tabSettings.Controls.Add(this.tglStartByDefault);
            this.tabSettings.Controls.Add(this.lblSBD);
            this.tabSettings.Controls.Add(this.metroPanel1);
            this.tabSettings.Controls.Add(this.menuSettings);
            this.tabSettings.HorizontalScrollbarBarColor = false;
            this.tabSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.tabSettings.HorizontalScrollbarSize = 0;
            this.tabSettings.Location = new System.Drawing.Point(4, 38);
            this.tabSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(826, 429);
            this.tabSettings.TabIndex = 0;
            this.tabSettings.Text = "Settings";
            this.tabSettings.VerticalScrollbarBarColor = false;
            this.tabSettings.VerticalScrollbarHighlightOnWheel = false;
            this.tabSettings.VerticalScrollbarSize = 0;
            // 
            // lblStartByDefault
            // 
            this.lblStartByDefault.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblStartByDefault.Location = new System.Drawing.Point(216, 66);
            this.lblStartByDefault.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartByDefault.Name = "lblStartByDefault";
            this.lblStartByDefault.Size = new System.Drawing.Size(68, 29);
            this.lblStartByDefault.TabIndex = 0;
            this.lblStartByDefault.Text = "False";
            this.lblStartByDefault.UseStyleColors = true;
            this.lblStartByDefault.Click += new System.EventHandler(this.lblStartByDefault_Click);
            // 
            // tglStartByDefault
            // 
            this.tglStartByDefault.AutoSize = true;
            this.tglStartByDefault.BackColor = System.Drawing.SystemColors.Control;
            this.tglStartByDefault.DisplayStatus = false;
            this.tglStartByDefault.Location = new System.Drawing.Point(284, 68);
            this.tglStartByDefault.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tglStartByDefault.Name = "tglStartByDefault";
            this.tglStartByDefault.Size = new System.Drawing.Size(50, 24);
            this.tglStartByDefault.TabIndex = 0;
            this.tglStartByDefault.Text = "Off";
            this.tglStartByDefault.UseCustomBackColor = true;
            this.tglStartByDefault.UseSelectable = true;
            this.tglStartByDefault.CheckedChanged += new System.EventHandler(this.tglStartByDefault_CheckedChanged);
            // 
            // lblSBD
            // 
            this.lblSBD.AutoSize = true;
            this.lblSBD.BackColor = System.Drawing.SystemColors.Control;
            this.lblSBD.Location = new System.Drawing.Point(15, 66);
            this.lblSBD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSBD.Name = "lblSBD";
            this.lblSBD.Size = new System.Drawing.Size(137, 19);
            this.lblSBD.TabIndex = 0;
            this.lblSBD.Text = "Start Proxy By Default";
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.lblInfo);
            this.metroPanel1.Controls.Add(this.styleCombobox);
            this.metroPanel1.Controls.Add(this.styleLabel);
            this.metroPanel1.Controls.Add(this.themeCombobox);
            this.metroPanel1.Controls.Add(this.themeLabel);
            this.metroPanel1.Controls.Add(this.lstServers);
            this.metroPanel1.Controls.Add(this.lblDefaultServer);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 15;
            this.metroPanel1.Location = new System.Drawing.Point(0, 51);
            this.metroPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(826, 378);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 15;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lblInfo.Location = new System.Drawing.Point(14, 326);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(308, 19);
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "K Relay must be re-opened for all settings to apply.";
            // 
            // styleCombobox
            // 
            this.styleCombobox.FormattingEnabled = true;
            this.styleCombobox.ItemHeight = 23;
            this.styleCombobox.Location = new System.Drawing.Point(96, 158);
            this.styleCombobox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.styleCombobox.MaxDropDownItems = 32;
            this.styleCombobox.Name = "styleCombobox";
            this.styleCombobox.Size = new System.Drawing.Size(259, 29);
            this.styleCombobox.TabIndex = 4;
            this.styleCombobox.UseSelectable = true;
            // 
            // styleLabel
            // 
            this.styleLabel.AutoSize = true;
            this.styleLabel.Location = new System.Drawing.Point(14, 166);
            this.styleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.styleLabel.Name = "styleLabel";
            this.styleLabel.Size = new System.Drawing.Size(36, 19);
            this.styleLabel.TabIndex = 5;
            this.styleLabel.Text = "Style";
            // 
            // themeCombobox
            // 
            this.themeCombobox.FormattingEnabled = true;
            this.themeCombobox.ItemHeight = 23;
            this.themeCombobox.Location = new System.Drawing.Point(96, 105);
            this.themeCombobox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.themeCombobox.MaxDropDownItems = 32;
            this.themeCombobox.Name = "themeCombobox";
            this.themeCombobox.Size = new System.Drawing.Size(259, 29);
            this.themeCombobox.TabIndex = 2;
            this.themeCombobox.UseSelectable = true;
            // 
            // themeLabel
            // 
            this.themeLabel.AutoSize = true;
            this.themeLabel.Location = new System.Drawing.Point(14, 112);
            this.themeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.themeLabel.Name = "themeLabel";
            this.themeLabel.Size = new System.Drawing.Size(49, 19);
            this.themeLabel.TabIndex = 3;
            this.themeLabel.Text = "Theme";
            // 
            // lstServers
            // 
            this.lstServers.FormattingEnabled = true;
            this.lstServers.ItemHeight = 23;
            this.lstServers.Items.AddRange(new object[] {
            "AsiaEast",
            "AsiaSouthEast",
            "EUEast",
            "EUNorth",
            "EUNorth2",
            "EUSouth",
            "EUSouthWest",
            "EUWest",
            "EUWest2",
            "USEast",
            "USEast2",
            "USEast3",
            "USMidWest",
            "USMidWest2",
            "USNorthWest",
            "USSouth",
            "USSouth2",
            "USSouth3",
            "USSouthWest",
            "USWest",
            "USWest2",
            "USWest3"});
            this.lstServers.Location = new System.Drawing.Point(160, 51);
            this.lstServers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstServers.MaxDropDownItems = 32;
            this.lstServers.Name = "lstServers";
            this.lstServers.Size = new System.Drawing.Size(194, 29);
            this.lstServers.TabIndex = 0;
            this.lstServers.UseSelectable = true;
            // 
            // lblDefaultServer
            // 
            this.lblDefaultServer.AutoSize = true;
            this.lblDefaultServer.Location = new System.Drawing.Point(14, 58);
            this.lblDefaultServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDefaultServer.Name = "lblDefaultServer";
            this.lblDefaultServer.Size = new System.Drawing.Size(92, 19);
            this.lblDefaultServer.TabIndex = 0;
            this.lblDefaultServer.Text = "Default Server";
            // 
            // menuSettings
            // 
            this.menuSettings.AutoSize = false;
            this.menuSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(113)))), ((int)(((byte)(189)))));
            this.menuSettings.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveSettings});
            this.menuSettings.Location = new System.Drawing.Point(0, 0);
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuSettings.Size = new System.Drawing.Size(826, 51);
            this.menuSettings.TabIndex = 0;
            this.menuSettings.Text = "menuStrip1";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(178, 45);
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // tabAbout
            // 
            this.tabAbout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabAbout.Controls.Add(this.pnlSide);
            this.tabAbout.Controls.Add(this.label1);
            this.tabAbout.Controls.Add(this.metroLabel5);
            this.tabAbout.Controls.Add(this.metroLabel4);
            this.tabAbout.Controls.Add(this.metroLabel2);
            this.tabAbout.Controls.Add(this.metroLabel1);
            this.tabAbout.Controls.Add(this.lblVersion);
            this.tabAbout.HorizontalScrollbarBarColor = true;
            this.tabAbout.HorizontalScrollbarHighlightOnWheel = false;
            this.tabAbout.HorizontalScrollbarSize = 15;
            this.tabAbout.Location = new System.Drawing.Point(4, 38);
            this.tabAbout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(826, 429);
            this.tabAbout.TabIndex = 0;
            this.tabAbout.Text = "About";
            this.tabAbout.UseStyleColors = true;
            this.tabAbout.UseVisualStyleBackColor = true;
            this.tabAbout.VerticalScrollbarBarColor = true;
            this.tabAbout.VerticalScrollbarHighlightOnWheel = false;
            this.tabAbout.VerticalScrollbarSize = 15;
            // 
            // pnlSide
            // 
            this.pnlSide.BackColor = System.Drawing.Color.Transparent;
            this.pnlSide.Controls.Add(this.pbxGang);
            this.pnlSide.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSide.Location = new System.Drawing.Point(242, 0);
            this.pnlSide.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(582, 427);
            this.pnlSide.TabIndex = 15;
            // 
            // pbxGang
            // 
            this.pbxGang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxGang.Image = ((System.Drawing.Image)(resources.GetObject("pbxGang.Image")));
            this.pbxGang.Location = new System.Drawing.Point(-24, 48);
            this.pbxGang.Name = "pbxGang";
            this.pbxGang.Size = new System.Drawing.Size(634, 309);
            this.pbxGang.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxGang.TabIndex = 6;
            this.pbxGang.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(3, -3);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 71);
            this.label1.TabIndex = 14;
            this.label1.Text = "K Relay";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.metroLabel5.Location = new System.Drawing.Point(15, 129);
            this.metroLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(97, 19);
            this.metroLabel5.TabIndex = 11;
            this.metroLabel5.Text = "Contributors:";
            this.metroLabel5.UseCustomForeColor = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(15, 158);
            this.metroLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(84, 133);
            this.metroLabel4.TabIndex = 10;
            this.metroLabel4.Text = "- MrNobody\r\n- ossimc82\r\n- Knorrex\r\n- Alde\r\n- 059\r\n- Kithio\r\n- ossimc82";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(15, 95);
            this.metroLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(135, 19);
            this.metroLabel2.TabIndex = 8;
            this.metroLabel2.Text = "- KrazyShank / Kronks";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.metroLabel1.Location = new System.Drawing.Point(15, 66);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(65, 19);
            this.metroLabel1.TabIndex = 7;
            this.metroLabel1.Text = "Creator:";
            this.metroLabel1.UseCustomForeColor = true;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(880, 14);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 19);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "v0.2.0";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblStatus.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblStatus.Location = new System.Drawing.Point(38, 523);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(426, 86);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Not Running";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmMainMetro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 615);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.lblStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuInfo;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmMainMetro";
            this.Padding = new System.Windows.Forms.Padding(30, 92, 30, 31);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "K Relay";
            this.Load += new System.EventHandler(this.FrmMainMetro_Load);
            this.tabMain.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.menuInfo.ResumeLayout(false);
            this.menuInfo.PerformLayout();
            this.tabPlugins.ResumeLayout(false);
            this.menuPlugins.ResumeLayout(false);
            this.menuPlugins.PerformLayout();
            this.tabPackets.ResumeLayout(false);
            this.menuPackets.ResumeLayout(false);
            this.menuPackets.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.menuSettings.ResumeLayout(false);
            this.menuSettings.PerformLayout();
            this.tabAbout.ResumeLayout(false);
            this.tabAbout.PerformLayout();
            this.pnlSide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxGang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl tabMain;
        private MetroFramework.Controls.MetroTabPage tabInfo;
        private MetroFramework.Controls.MetroTabPage tabPlugins;
        private MetroFramework.Controls.MetroTabPage tabPackets;
        private MetroFramework.Controls.MetroTabPage tabSettings;
        private MetroFramework.Controls.MetroTabPage tabAbout;
        private MetroFramework.Controls.MetroTextBox tbxLog;
        private System.Windows.Forms.MenuStrip menuInfo;
        private System.Windows.Forms.ToolStripMenuItem btnToggleProxy;
        private System.Windows.Forms.ToolStripMenuItem btnSaveLog;
        private System.Windows.Forms.ToolStripMenuItem btnClearLog;
        private MetroFramework.Controls.MetroLabel lblStatus;
        private K_Relay.Controls.MetroListBox listPackets;
        private K_Relay.Controls.MetroListBox listPlugins;
        private System.Windows.Forms.MenuStrip menuPlugins;
        private System.Windows.Forms.MenuStrip menuPackets;
        private System.Windows.Forms.MenuStrip menuSettings;
        private System.Windows.Forms.ToolStripMenuItem openPluginFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPacketFolderToolStripMenuItem;
        private K_Relay.Controls.MetroRichTextBox tbxPluginInfo;
        private K_Relay.Controls.MetroRichTextBox tbxPacketInfo;
        private MetroFramework.Controls.MetroLabel lblSBD;
        private MetroFramework.Controls.MetroToggle tglStartByDefault;
        private System.Windows.Forms.ToolStripMenuItem btnSaveSettings;
        private MetroFramework.Controls.MetroLabel lblStartByDefault;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroComboBox lstServers;
        private MetroFramework.Controls.MetroLabel lblDefaultServer;
        private System.Windows.Forms.PictureBox pbxGang;
        private MetroFramework.Controls.MetroLabel lblVersion;
        private MetroFramework.Controls.MetroComboBox styleCombobox;
        private MetroFramework.Controls.MetroLabel styleLabel;
        private MetroFramework.Controls.MetroComboBox themeCombobox;
        private MetroFramework.Controls.MetroLabel themeLabel;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlSide;
        private MetroFramework.Controls.MetroLabel lblInfo;
    }
}