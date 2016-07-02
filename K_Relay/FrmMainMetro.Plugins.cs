using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using K_Relay.Util;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Utilities;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Drawing;

namespace K_Relay
{
    partial class FrmMainMetro
    {
        private Dictionary<string, IPlugin> _pluginNameMap = new Dictionary<string, IPlugin>();

        private void InitPlugins()
        {
            string pDir = Application.StartupPath + @"\Plugins\";

            if (!Directory.Exists(pDir))
            {
                Directory.CreateDirectory(pDir);
                MetroMessageBox.Show(this, string.Format("Plugin directory not found! Directory created at '{0}'.", pDir), "Directory Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PluginUtils.Log("Interface", "Plugin directory not found! Directory created at '{0}'.", pDir);
                return;
            }

            foreach (string pPath in Directory.GetFiles(pDir, "*.dll", SearchOption.AllDirectories))
            {
                if (new FileInfo(pPath).Name.Contains("Lib K Relay")) continue;
                Assembly pAssembly = Assembly.LoadFrom(pPath);

                foreach (Type pType in pAssembly.GetTypes())
                {
                    if (pType.IsPublic && !pType.IsAbstract)
                    {
                        try
                        {
                            Type tInterface = pType.GetInterface("Lib_K_Relay.Interface.IPlugin");

                            if (tInterface != null)
                                AttachPlugin(pType);
                        }
                        catch (Exception e)
                        {
                            MetroMessageBox.Show(this, "Failed to load plugin " + pPath + "!\n" + e.Message, "K Relay",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnOpenPluginFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + @"\Plugins\");
            }
            catch (Win32Exception)
            {
                MetroMessageBox.Show(this,
                    string.Format(
                        "File not found!\n\nThe directory '{0}' could not be found.\nPlease make sure it exists and Try Again.",
                        Application.StartupPath + @"\Plugins\"), "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.ToString(), "Error - " + ex.GetType().Name, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        
        private void listPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPlugins.ListBox.SelectedItem != null)
            {
                string key = (string)listPlugins.ListBox.SelectedItem;
                IPlugin selected = _pluginNameMap[key];
                PluginDescriptionView(selected);
            }
        }

        public void AttachPlugin(Type type)
        {
            IPlugin instance = (IPlugin)Activator.CreateInstance(type);
            string name = instance.GetName();
            instance.Initialize(_proxy);

            listPlugins.ListBox.Items.Add(name);
            _pluginNameMap.Add(name, instance);

            PluginUtils.Log("Interface", "Loaded and attached {0}.", name);
        }

        private void PluginDescriptionView(IPlugin plugin)
        {
            string name = plugin.GetName();
            string author = plugin.GetAuthor();
            string description = plugin.GetDescription();
            string type = plugin.GetType().ToString();
            string[] commands = plugin.GetCommands();

            tbxPluginInfo.Clear();
            tbxPluginInfo.AppendText("Plugin: ", Color.DodgerBlue, true);
            tbxPluginInfo.AppendText(name, Color.Empty, false);
            tbxPluginInfo.AppendText("\nAuthor: ", Color.DodgerBlue, true);
            tbxPluginInfo.AppendText(author, Color.Empty, false);
            tbxPluginInfo.AppendText("\nClassName: ", Color.DodgerBlue, true);
            tbxPluginInfo.AppendText(type, Color.Empty, false);
            tbxPluginInfo.AppendText("\n\nDescription:\n", Color.DodgerBlue, true);
            tbxPluginInfo.AppendText(description, Color.Empty, false);
            if (commands.Count() > 0)
            {
                tbxPluginInfo.AppendText("\n\nCommands:", Color.DodgerBlue, true);
                foreach (string command in commands)
                    tbxPluginInfo.AppendText("\n  " + command, Color.Empty, false);
            }
        }
    }
}
