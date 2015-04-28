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

namespace K_Relay
{
    partial class FrmMainMetro
    {
        private Dictionary<string, IPlugin> _pluginNameMap = new Dictionary<string, IPlugin>();

        private void InitPlugins()
        {
            string pDir = Serializer.DEBUGGetSolutionRoot() + @"\Plugins\";

            if (Config.Default.UseInternalReconnectHandler)
                AttachPlugin(typeof (ReconnectHandler));

            if (!Directory.Exists(pDir))
            {
                Directory.CreateDirectory(pDir);
                MetroMessageBox.Show(this, string.Format("Plugin directory not found! Directory created at '{0}'.", pDir), "Directory Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("[Plugin Manager] Plugin directory not found! Directory created at '{0}'.", pDir);
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
                Process.Start(Serializer.DEBUGGetSolutionRoot() + @"\Plugins\");
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                    MetroMessageBox.Show(this,
                        string.Format(
                            "File not found!\n\nThe directory '{0}' could not be found.\nPlease make sure it exists and Try Again.",
                            Serializer.DEBUGGetSolutionRoot() + @"\Plugins\"), "Error!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                else
                    MetroMessageBox.Show(this, ex.ToString(), "Error - " + ex.GetType().Name, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        private void listPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPlugins.SelectedItem != null)
            {
                string key = (string)listPlugins.SelectedItem;
                IPlugin selected = _pluginNameMap[key];
                PluginDescriptionView(selected);
            }
        }

        public void AttachPlugin(Type type)
        {
            IPlugin instance = (IPlugin)Activator.CreateInstance(type);
            string name = instance.GetName();
            instance.Initialize(_proxy);

            listPlugins.Items.Add(name);

            _pluginNameMap.Add(name, instance);

            Console.WriteLine("[Plugin Manager] Loaded and attached {0}", name);
        }

        private void PluginDescriptionView(IPlugin plugin)
        {
            string name = plugin.GetName();
            string author = plugin.GetAuthor();
            string description = plugin.GetDescription();
            string type = plugin.GetType().ToString();
            string[] commands = plugin.GetCommands();

            tbxPluginInfo.Clear();

            AppendText(tbxPluginInfo, "Plugin: ", Color.DodgerBlue, true);
            AppendText(tbxPluginInfo, name, Color.Black, false);
            AppendText(tbxPluginInfo, "\nAuthor: ", Color.DodgerBlue, true);
            AppendText(tbxPluginInfo, author, Color.Black, false);
            AppendText(tbxPluginInfo, "\nClassName: ", Color.DodgerBlue, true);
            AppendText(tbxPluginInfo, type, Color.Black, false);
            AppendText(tbxPluginInfo, "\n\nDescription:\n", Color.DodgerBlue, true);
            AppendText(tbxPluginInfo, description, Color.Black, false);
            if (commands.Count() > 0)
            {
                AppendText(tbxPluginInfo, "\n\nCommands:", Color.DodgerBlue, true);
                foreach (string command in commands)
                {
                    AppendText(tbxPluginInfo, "\n  " + command, Color.Black, false);
                }
            }
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
