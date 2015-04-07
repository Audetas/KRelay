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
            this.chkDisableAllMessages = new System.Windows.Forms.CheckBox();
            this.chkChangeMessageToFiltered = new System.Windows.Forms.CheckBox();
            this.chkAutoIgnore = new System.Windows.Forms.CheckBox();
            this.chkAutoIgnorePMs = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuSettings.SuspendLayout();
            this.groupOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuSettings
            // 
            this.menuSettings.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuSettings.Location = new System.Drawing.Point(0, 0);
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(958, 40);
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
            this.chkSpamFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSpamFilter.Location = new System.Drawing.Point(12, 58);
            this.chkSpamFilter.Name = "chkSpamFilter";
            this.chkSpamFilter.Size = new System.Drawing.Size(234, 29);
            this.chkSpamFilter.TabIndex = 2;
            this.chkSpamFilter.Text = "Enable Chat Filter";
            this.chkSpamFilter.UseVisualStyleBackColor = true;
            // 
            // tbxBlacklist
            // 
            this.tbxBlacklist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxBlacklist.Location = new System.Drawing.Point(12, 93);
            this.tbxBlacklist.Name = "tbxBlacklist";
            this.tbxBlacklist.Size = new System.Drawing.Size(472, 294);
            this.tbxBlacklist.TabIndex = 3;
            this.tbxBlacklist.Text = "";
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(7, 390);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(677, 25);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "Blacklist: Seperate items with a comma. Items are not case sensative.";
            // 
            // groupOptions
            // 
            this.groupOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupOptions.Controls.Add(this.chkDisableAllMessages);
            this.groupOptions.Controls.Add(this.chkChangeMessageToFiltered);
            this.groupOptions.Controls.Add(this.chkAutoIgnore);
            this.groupOptions.Controls.Add(this.chkAutoIgnorePMs);
            this.groupOptions.Location = new System.Drawing.Point(490, 79);
            this.groupOptions.Name = "groupOptions";
            this.groupOptions.Size = new System.Drawing.Size(456, 308);
            this.groupOptions.TabIndex = 6;
            this.groupOptions.TabStop = false;
            // 
            // chkDisableAllMessages
            // 
            this.chkDisableAllMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDisableAllMessages.AutoSize = true;
            this.chkDisableAllMessages.Location = new System.Drawing.Point(6, 273);
            this.chkDisableAllMessages.Name = "chkDisableAllMessages";
            this.chkDisableAllMessages.Size = new System.Drawing.Size(367, 29);
            this.chkDisableAllMessages.TabIndex = 14;
            this.chkDisableAllMessages.Text = "Disable all messages except PMs";
            this.chkDisableAllMessages.UseVisualStyleBackColor = true;
            // 
            // chkChangeMessageToFiltered
            // 
            this.chkChangeMessageToFiltered.AutoSize = true;
            this.chkChangeMessageToFiltered.Location = new System.Drawing.Point(6, 88);
            this.chkChangeMessageToFiltered.Name = "chkChangeMessageToFiltered";
            this.chkChangeMessageToFiltered.Size = new System.Drawing.Size(361, 29);
            this.chkChangeMessageToFiltered.TabIndex = 13;
            this.chkChangeMessageToFiltered.Text = "Replace spam messages with \'...\'";
            this.chkChangeMessageToFiltered.UseVisualStyleBackColor = true;
            // 
            // chkAutoIgnore
            // 
            this.chkAutoIgnore.AutoSize = true;
            this.chkAutoIgnore.Location = new System.Drawing.Point(6, 53);
            this.chkAutoIgnore.Name = "chkAutoIgnore";
            this.chkAutoIgnore.Size = new System.Drawing.Size(409, 29);
            this.chkAutoIgnore.TabIndex = 12;
            this.chkAutoIgnore.Text = "Auto Ignore if message contains spam";
            this.chkAutoIgnore.UseVisualStyleBackColor = true;
            // 
            // chkAutoIgnorePMs
            // 
            this.chkAutoIgnorePMs.AutoSize = true;
            this.chkAutoIgnorePMs.Location = new System.Drawing.Point(6, 18);
            this.chkAutoIgnorePMs.Name = "chkAutoIgnorePMs";
            this.chkAutoIgnorePMs.Size = new System.Drawing.Size(354, 29);
            this.chkAutoIgnorePMs.TabIndex = 11;
            this.chkAutoIgnorePMs.Text = "Auto Ignore if PM contains spam";
            this.chkAutoIgnorePMs.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(485, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Other Options:";
            // 
            // FrmChatAssistSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 423);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupOptions);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.chkSpamFilter);
            this.Controls.Add(this.tbxBlacklist);
            this.Controls.Add(this.menuSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuSettings;
            this.Name = "FrmChatAssistSettings";
            this.Text = "Chat Assist Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmChatAssistSettings_FormClosing);
            this.menuSettings.ResumeLayout(false);
            this.menuSettings.PerformLayout();
            this.groupOptions.ResumeLayout(false);
            this.groupOptions.PerformLayout();
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
    }
}