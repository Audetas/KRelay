namespace ChatAssist
{
    partial class FrmChatAssistSettings
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
            this.menuSettings = new System.Windows.Forms.MenuStrip();
            this.btnDone = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReset = new System.Windows.Forms.ToolStripMenuItem();
            this.chkSpamFilter = new System.Windows.Forms.CheckBox();
            this.tbxBlacklist = new System.Windows.Forms.RichTextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupOptions = new System.Windows.Forms.GroupBox();
            this.chkLogChat = new System.Windows.Forms.CheckBox();
            this.chkFilterNPCText = new System.Windows.Forms.CheckBox();
            this.chkDisableAllMessages = new System.Windows.Forms.CheckBox();
            this.chkChangeMessageToFiltered = new System.Windows.Forms.CheckBox();
            this.chkAutoIgnore = new System.Windows.Forms.CheckBox();
            this.chkAutoIgnorePMs = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UpDownStarFilter = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.UpDownPMFilter = new System.Windows.Forms.NumericUpDown();
            this.groupOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownStarFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownPMFilter)).BeginInit();
            this.SuspendLayout();
            // 
            // menuSettings
            // 
            this.menuSettings.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuSettings.Location = new System.Drawing.Point(0, 0);
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuSettings.Size = new System.Drawing.Size(479, 24);
            this.menuSettings.TabIndex = 0;
            this.menuSettings.Text = "menuSettings";
            // 
            // btnDone
            // 
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(85, 36);
            this.btnDone.Text = "Done";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnReset
            // 
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(209, 36);
            this.btnReset.Text = "Reset to Defaults";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // chkSpamFilter
            // 
            this.chkSpamFilter.AutoSize = true;
            this.chkSpamFilter.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSpamFilter.Location = new System.Drawing.Point(6, 30);
            this.chkSpamFilter.Margin = new System.Windows.Forms.Padding(2);
            this.chkSpamFilter.Name = "chkSpamFilter";
            this.chkSpamFilter.Size = new System.Drawing.Size(122, 17);
            this.chkSpamFilter.TabIndex = 2;
            this.chkSpamFilter.Text = "Enable Spam Filter";
            this.chkSpamFilter.UseVisualStyleBackColor = true;
            // 
            // tbxBlacklist
            // 
            this.tbxBlacklist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxBlacklist.Location = new System.Drawing.Point(6, 48);
            this.tbxBlacklist.Margin = new System.Windows.Forms.Padding(2);
            this.tbxBlacklist.Name = "tbxBlacklist";
            this.tbxBlacklist.Size = new System.Drawing.Size(238, 155);
            this.tbxBlacklist.TabIndex = 3;
            this.tbxBlacklist.Text = "";
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(4, 203);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(335, 13);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "Blacklist: Seperate items with new lines. Items are not case sensative.";
            // 
            // groupOptions
            // 
            this.groupOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupOptions.Controls.Add(this.UpDownPMFilter);
            this.groupOptions.Controls.Add(this.label3);
            this.groupOptions.Controls.Add(this.UpDownStarFilter);
            this.groupOptions.Controls.Add(this.label2);
            this.groupOptions.Controls.Add(this.chkLogChat);
            this.groupOptions.Controls.Add(this.chkFilterNPCText);
            this.groupOptions.Controls.Add(this.chkDisableAllMessages);
            this.groupOptions.Controls.Add(this.chkChangeMessageToFiltered);
            this.groupOptions.Controls.Add(this.chkAutoIgnore);
            this.groupOptions.Controls.Add(this.chkAutoIgnorePMs);
            this.groupOptions.Location = new System.Drawing.Point(245, 41);
            this.groupOptions.Margin = new System.Windows.Forms.Padding(2);
            this.groupOptions.Name = "groupOptions";
            this.groupOptions.Padding = new System.Windows.Forms.Padding(2);
            this.groupOptions.Size = new System.Drawing.Size(228, 160);
            this.groupOptions.TabIndex = 6;
            this.groupOptions.TabStop = false;
            // 
            // chkLogChat
            // 
            this.chkLogChat.AutoSize = true;
            this.chkLogChat.Location = new System.Drawing.Point(4, 88);
            this.chkLogChat.Margin = new System.Windows.Forms.Padding(2);
            this.chkLogChat.Name = "chkLogChat";
            this.chkLogChat.Size = new System.Drawing.Size(68, 17);
            this.chkLogChat.TabIndex = 16;
            this.chkLogChat.Text = "Log chat";
            this.chkLogChat.UseVisualStyleBackColor = true;
            // 
            // chkFilterNPCText
            // 
            this.chkFilterNPCText.AutoSize = true;
            this.chkFilterNPCText.Location = new System.Drawing.Point(4, 67);
            this.chkFilterNPCText.Margin = new System.Windows.Forms.Padding(2);
            this.chkFilterNPCText.Name = "chkFilterNPCText";
            this.chkFilterNPCText.Size = new System.Drawing.Size(93, 17);
            this.chkFilterNPCText.TabIndex = 15;
            this.chkFilterNPCText.Text = "Filter NPC text";
            this.chkFilterNPCText.UseVisualStyleBackColor = true;
            // 
            // chkDisableAllMessages
            // 
            this.chkDisableAllMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDisableAllMessages.AutoSize = true;
            this.chkDisableAllMessages.Location = new System.Drawing.Point(3, 140);
            this.chkDisableAllMessages.Margin = new System.Windows.Forms.Padding(2);
            this.chkDisableAllMessages.Name = "chkDisableAllMessages";
            this.chkDisableAllMessages.Size = new System.Drawing.Size(183, 17);
            this.chkDisableAllMessages.TabIndex = 14;
            this.chkDisableAllMessages.Text = "Disable all messages except PMs";
            this.chkDisableAllMessages.UseVisualStyleBackColor = true;
            // 
            // chkChangeMessageToFiltered
            // 
            this.chkChangeMessageToFiltered.AutoSize = true;
            this.chkChangeMessageToFiltered.Location = new System.Drawing.Point(3, 46);
            this.chkChangeMessageToFiltered.Margin = new System.Windows.Forms.Padding(2);
            this.chkChangeMessageToFiltered.Name = "chkChangeMessageToFiltered";
            this.chkChangeMessageToFiltered.Size = new System.Drawing.Size(182, 17);
            this.chkChangeMessageToFiltered.TabIndex = 13;
            this.chkChangeMessageToFiltered.Text = "Replace spam messages with \'...\'";
            this.chkChangeMessageToFiltered.UseVisualStyleBackColor = true;
            // 
            // chkAutoIgnore
            // 
            this.chkAutoIgnore.AutoSize = true;
            this.chkAutoIgnore.Location = new System.Drawing.Point(3, 28);
            this.chkAutoIgnore.Margin = new System.Windows.Forms.Padding(2);
            this.chkAutoIgnore.Name = "chkAutoIgnore";
            this.chkAutoIgnore.Size = new System.Drawing.Size(205, 17);
            this.chkAutoIgnore.TabIndex = 12;
            this.chkAutoIgnore.Text = "Auto Ignore if message contains spam";
            this.chkAutoIgnore.UseVisualStyleBackColor = true;
            // 
            // chkAutoIgnorePMs
            // 
            this.chkAutoIgnorePMs.AutoSize = true;
            this.chkAutoIgnorePMs.Location = new System.Drawing.Point(3, 9);
            this.chkAutoIgnorePMs.Margin = new System.Windows.Forms.Padding(2);
            this.chkAutoIgnorePMs.Name = "chkAutoIgnorePMs";
            this.chkAutoIgnorePMs.Size = new System.Drawing.Size(179, 17);
            this.chkAutoIgnorePMs.TabIndex = 11;
            this.chkAutoIgnorePMs.Text = "Auto Ignore if PM contains spam";
            this.chkAutoIgnorePMs.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(242, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Other Options:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Start filter:";
            // 
            // UpDownStarFilter
            // 
            this.UpDownStarFilter.Location = new System.Drawing.Point(64, 109);
            this.UpDownStarFilter.Name = "UpDownStarFilter";
            this.UpDownStarFilter.Size = new System.Drawing.Size(41, 20);
            this.UpDownStarFilter.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "PM filter:";
            // 
            // UpDownPMFilter
            // 
            this.UpDownPMFilter.Location = new System.Drawing.Point(165, 109);
            this.UpDownPMFilter.Name = "UpDownPMFilter";
            this.UpDownPMFilter.Size = new System.Drawing.Size(39, 20);
            this.UpDownPMFilter.TabIndex = 20;
            // 
            // FrmChatAssistSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 220);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupOptions);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.chkSpamFilter);
            this.Controls.Add(this.tbxBlacklist);
            this.Controls.Add(this.menuSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuSettings;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmChatAssistSettings";
            this.Text = "Chat Assist Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmChatAssistSettings_FormClosing);
            this.groupOptions.ResumeLayout(false);
            this.groupOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownStarFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownPMFilter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuSettings;
        private System.Windows.Forms.ToolStripMenuItem btnDone;
        private System.Windows.Forms.ToolStripMenuItem btnReset;
        private System.Windows.Forms.CheckBox chkSpamFilter;
        private System.Windows.Forms.RichTextBox tbxBlacklist;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.GroupBox groupOptions;
        private System.Windows.Forms.CheckBox chkDisableAllMessages;
        private System.Windows.Forms.CheckBox chkChangeMessageToFiltered;
        private System.Windows.Forms.CheckBox chkAutoIgnore;
        private System.Windows.Forms.CheckBox chkAutoIgnorePMs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkFilterNPCText;
        private System.Windows.Forms.CheckBox chkLogChat;
        private System.Windows.Forms.NumericUpDown UpDownStarFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown UpDownPMFilter;
    }
}