using K_Relay.Util;
using Lib_K_Relay.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_Relay
{
    partial class FrmMain
    {
        private Dictionary<string, IPlugin> _pluginNameMap = new Dictionary<string, IPlugin>();

        private void InitPlugins()
        {
            Console.WriteLine("[Plugin Manager] Looking for plugins in {0}", Config.Default.PluginDirectory);
            string pluginDirectory = Config.Default.PluginDirectory.ToLower().Replace("%startuppath%", Application.StartupPath);

            if (!Directory.Exists(pluginDirectory))
            {
                Console.WriteLine("[Plugin Manager] Plugin directory '{0}' does not exist! No plugins will be loaded.", Config.Default.PluginDirectory);
                return;
            }

            foreach (string pluginPath in Directory.GetFiles(pluginDirectory, "*.dll", SearchOption.AllDirectories))
            {
                Assembly pluginAssembly = Assembly.LoadFrom(pluginPath);

                foreach (Type pluginType in pluginAssembly.GetTypes())
                {
                    if (pluginType.IsPublic && !pluginType.IsAbstract)
                    {
                        try
                        {
                            Type typeInterface = pluginType.GetInterface("Lib_K_Relay.Interface.IPlugin");

                            if (typeInterface != null)
                                AttachPlugin(pluginType);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Failed to load plugin " + pluginPath + "!\n" + e.Message,
                                "K Relay", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            // DEBUG
            //AttachPlugin(typeof(PacketDebugger));

            if (Config.Default.UseInternalReconnectHandler)
                AttachPlugin(typeof(ReconnectHandler));
        }

        private void btnOpenPluginFolder_Click(object sender, EventArgs e)
        {
            Process.Start(
                Config.Default.PluginDirectory.ToLower().Replace("%startuppath%", Application.StartupPath));
        }

        protected void treePlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (treePlugins.SelectedItem != null)
            {
                string key = (string)treePlugins.SelectedItem;
                IPlugin selected = _pluginNameMap[key];
                PluginDescriptionView(selected);
            }
        }

        public void AttachPlugin(Type type)
        {
            IPlugin instance = (IPlugin)Activator.CreateInstance(type);
            string name = instance.GetName();
            instance.Initialize(_proxy);

            treePlugins.Items.Add(name);

            _pluginNameMap.Add(name, instance);

            Console.WriteLine("[Plugin Manager] Loaded and attached {0}", name);
        }

        private void PluginDescriptionView(IPlugin plugin)
        {
            string name = plugin.GetName();
            string author = plugin.GetAuthor();
            string description = plugin.GetDescription();
            string type = plugin.GetType().ToString();

            tbxPluginInfo.Clear();

            AppendText(tbxPluginInfo, "Plugin: ", Color.DodgerBlue, true);
            AppendText(tbxPluginInfo, name, Color.Black, false);
            AppendText(tbxPluginInfo, "\nAuthor: ", Color.DodgerBlue, true);
            AppendText(tbxPluginInfo, author, Color.Black, false);
            AppendText(tbxPluginInfo, "\nClassName: ", Color.DodgerBlue, true);
            AppendText(tbxPluginInfo, type, Color.Black, false);
            AppendText(tbxPluginInfo, "\n\nDescription:\n", Color.DodgerBlue, true);
            AppendText(tbxPluginInfo, description, Color.Black, false);
 
        }

        public static void AppendText(RichTextBox box, string text, Color color, Boolean bold)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            if (bold)
                box.SelectionFont = new Font(box.Font, FontStyle.Bold);
            else
                box.SelectionFont = new Font(box.Font, FontStyle.Regular);
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
