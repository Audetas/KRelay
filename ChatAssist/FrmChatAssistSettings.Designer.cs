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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnDone = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReset = new System.Windows.Forms.ToolStripMenuItem();
            this.chkSpamFilter = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupOptions = new System.Windows.Forms.GroupBox();
            this.chkDisableAllMessages = new System.Windows.Forms.CheckBox();
            this.chkChangeMessageToFiltered = new System.Windows.Forms.CheckBox();
            this.chkAutoIgnore = new System.Windows.Forms.CheckBox();
            this.chkAutoIgnorePMs = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDone,
            this.btnReset});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(958, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuSettings";
            // 
            // btnDone
            // 
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(85, 36);
            this.btnDone.Text = "Done";
            // 
            // btnReset
            // 
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(209, 36);
            this.btnReset.Text = "Reset to Defaults";
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
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 93);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(472, 294);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
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
            this.chkChangeMessageToFiltered.Size = new System.Drawing.Size(415, 29);
            this.chkChangeMessageToFiltered.TabIndex = 13;
            this.chkChangeMessageToFiltered.Text = "Replace spam messages with \'Filtered\'";
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
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmChatAssistSettings";
            this.Text = "Chat Assist Settings";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupOptions.ResumeLayout(false);
            this.groupOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnDone;
        private System.Windows.Forms.ToolStripMenuItem btnReset;
        private System.Windows.Forms.CheckBox chkSpamFilter;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.GroupBox groupOptions;
        private System.Windows.Forms.CheckBox chkDisableAllMessages;
        private System.Windows.Forms.CheckBox chkChangeMessageToFiltered;
        private System.Windows.Forms.CheckBox chkAutoIgnore;
        private System.Windows.Forms.CheckBox chkAutoIgnorePMs;
        private System.Windows.Forms.Label label1;
    }
}