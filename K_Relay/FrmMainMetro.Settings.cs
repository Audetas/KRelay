using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib_K_Relay.Utilities;
using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Forms;

namespace K_Relay
{
    partial class FrmMainMetro
    {
        private FixedStyleManager m_themeManager;

        private void InitSettings()
        {
            this.m_themeManager = new FixedStyleManager(this);

            this.themeCombobox.Items.AddRange(Enum.GetNames(typeof(MetroThemeStyle)));
            this.styleCombobox.Items.AddRange(Enum.GetNames(typeof(MetroColorStyle)));

            this.themeCombobox.SelectedValueChanged += themeCombobox_SelectedValueChanged;
            this.styleCombobox.SelectedValueChanged += styleCombobox_SelectedValueChanged;

            this.themeCombobox.SelectedItem = (object)Config.Default.Theme.ToString();
            this.styleCombobox.SelectedItem = (object)Config.Default.Style.ToString();

            tglStartByDefault.Checked = Config.Default.StartProxyByDefault;
            tglUseInternalReconnectHandler.Checked = Config.Default.UseInternalReconnectHandler;
            lstServers.Items.AddRange(Serializer.Servers.Keys.ToArray());
            lstServers.SelectedItem = Config.Default.DefaultServerName;
        }

        private void styleCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.m_themeManager.Style = (MetroColorStyle)Enum.Parse(typeof(MetroColorStyle), (string)styleCombobox.SelectedItem, true);
        }

        private void themeCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.m_themeManager.Theme = (MetroThemeStyle)Enum.Parse(typeof(MetroThemeStyle), (string)themeCombobox.SelectedItem, true);
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            Config.Default.StartProxyByDefault = tglStartByDefault.Checked;
            Config.Default.UseInternalReconnectHandler = tglUseInternalReconnectHandler.Checked;
            Config.Default.DefaultServerName = lstServers.SelectedItem.ToString();
            Config.Default.Theme = (MetroThemeStyle)Enum.Parse(typeof(MetroThemeStyle), (string)themeCombobox.SelectedItem, true);
            Config.Default.Style = (MetroColorStyle)Enum.Parse(typeof(MetroColorStyle), (string)styleCombobox.SelectedItem, true);
            Config.Default.Save();

            MetroMessageBox.Show(this, "\nYour settings have been saved.", "Save Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private class FixedStyleManager
        {
            public event EventHandler OnThemeChanged;
            public event EventHandler OnStyleChanged;

            private MetroStyleManager m_manager;

            private MetroColorStyle m_colorStyle;
            private MetroThemeStyle m_themeStyle;


            public FixedStyleManager(MetroForm form)
            {
                this.m_manager = new MetroStyleManager(form.Container);
                this.m_manager.Owner = form;
            }

            public MetroColorStyle Style
            {
                get { return this.m_colorStyle; }
                set
                {
                    this.m_colorStyle = value;
                    Update();
                    if (OnStyleChanged != null) OnStyleChanged(this, new EventArgs());
                }
            }

            public MetroThemeStyle Theme
            {
                get { return this.m_themeStyle; }
                set
                {
                    this.m_themeStyle = value;
                    Update();
                    if (OnThemeChanged != null) OnThemeChanged(this, new EventArgs());
                }
            }

            public void Update()
            {
                (this.m_manager.Owner as MetroForm).Theme = this.m_themeStyle;
                (this.m_manager.Owner as MetroForm).Style = this.m_colorStyle;

                this.m_manager.Theme = this.m_themeStyle;
                this.m_manager.Style = this.m_colorStyle;
            }
        }
    }
}
