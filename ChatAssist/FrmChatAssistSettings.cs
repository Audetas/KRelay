using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatAssist
{
    public partial class FrmChatAssistSettings : Form
    {
        public FrmChatAssistSettings()
        {
            InitializeComponent();
            chkAutoIgnore.Checked = ChatAssistConfig.Default.AutoIgnoreSpamMessage;
            chkAutoIgnorePMs.Checked = ChatAssistConfig.Default.AutoIgnoreSpamPM;
            chkChangeMessageToFiltered.Checked = ChatAssistConfig.Default.CensorSpamMessages;
            chkDisableAllMessages.Checked = ChatAssistConfig.Default.DisableMessages;
            chkSpamFilter.Checked = ChatAssistConfig.Default.EnableSpamFilter;
            chkFilterNPCText.Checked = ChatAssistConfig.Default.EnableNPCFilter;
            chkLogChat.Checked = ChatAssistConfig.Default.LogChat;
            UpDownStarFilter.Value = ChatAssistConfig.Default.StarFilter;
            UpDownPMFilter.Value = ChatAssistConfig.Default.StarFilterPM;

            foreach (string filter in ChatAssistConfig.Default.Blacklist)
                tbxBlacklist.Text += filter + "\n";
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            ChatAssistConfig.Default.Save();
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset your settings?", "Chat Assist", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ChatAssistConfig.Default.Reset();
                ChatAssistConfig.Default.Save();
                this.Close();
            }
        }

        private void FrmChatAssistSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChatAssistConfig.Default.Enabled = chkSpamFilter.Checked;
            ChatAssistConfig.Default.AutoIgnoreSpamMessage = chkAutoIgnore.Checked;
            ChatAssistConfig.Default.AutoIgnoreSpamPM = chkAutoIgnorePMs.Checked;
            ChatAssistConfig.Default.CensorSpamMessages = chkChangeMessageToFiltered.Checked;
            ChatAssistConfig.Default.DisableMessages = chkDisableAllMessages.Checked;
            ChatAssistConfig.Default.Blacklist.Clear();
            ChatAssistConfig.Default.EnableNPCFilter = chkFilterNPCText.Checked;
            ChatAssistConfig.Default.LogChat = chkLogChat.Checked;
            ChatAssistConfig.Default.StarFilter = (int)UpDownStarFilter.Value;
            ChatAssistConfig.Default.StarFilterPM = (int)UpDownPMFilter.Value;
            ChatAssistConfig.Default.Blacklist.AddRange(tbxBlacklist.Text.Split(
                new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
            ChatAssistConfig.Default.Save();
        }
    }
}
