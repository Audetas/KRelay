using LibKRelay;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KRelay
{
    class PluginLoader
    {
        public Dictionary<Type, Plugin> LoadedPlugins { get; private set; }
        private ClientListener listener;

        public PluginLoader(ClientListener cl)
        {
            LoadedPlugins = new Dictionary<Type, Plugin>();
            listener = cl;
        }

        public void Load(string directory)
        {
            foreach (string file in Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories))
                Attach(file);
        }

        public void Attach(string assemblyPath)
        {
            Assembly assembly = Assembly.LoadFile(assemblyPath);
            Type tPlugin = typeof(Plugin);
            
            foreach (TypeInfo ti in assembly.GetTypes())
            {
                if (ti.IsPublic && ti.BaseType == tPlugin)
                {
                    Plugin instance = (Plugin)Activator.CreateInstance(ti.AsType());
                    instance.Initialize(listener);
                    LoadedPlugins.Add(ti.AsType(), instance);
                    ConsoleEx.Ok("Loaded: " + instance.Name + ", by: " + instance.Author);
                }
            }
        }
    }
}
