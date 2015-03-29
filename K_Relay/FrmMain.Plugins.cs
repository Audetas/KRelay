using Lib_K_Relay.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Dictionary<string, string> _pluginDescriptionMap = new Dictionary<string, string>();

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
                            {
                                IPlugin instance = (IPlugin)Activator.CreateInstance(pluginType);
                                string name = instance.GetName();
                                string author = instance.GetAuthor();
                                string description = instance.GetDescription();
                                instance.Initialize(_proxy);

                                TreeNode pluginNode = new TreeNode(name);
                                pluginNode.Nodes.Add("By: " + author);
                                pluginNode.Nodes.Add("Plugin Classname: " + pluginType.ToString());
                                treePlugins.Nodes.Add(pluginNode);

                                _pluginDescriptionMap.Add(name + author, description);

                                Console.WriteLine("[Plugin Manager] Loaded and attached {0}", name);
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Failed to load plugin " + pluginPath + "!\n" + e.Message,
                                "K Relay", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnOpenPluginFolder_Click(object sender, EventArgs e)
        {
            Process.Start(
                Config.Default.PluginDirectory.ToLower().Replace("%startuppath%", Application.StartupPath));
        }

        private void treePlugins_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0) // Ensure its a parent node
            {
                string key = e.Node.Text + e.Node.Nodes[0].Text.Replace("By: ", "");
                tbxPluginInfo.Text = _pluginDescriptionMap[key];
            }
        }
    }
}
