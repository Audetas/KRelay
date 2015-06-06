//The MIT License (MIT)
//
//Copyright (c) 2015 Fabian Fischer
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapRipper
{
    public class Plugin : IPlugin
    {
        private bool _enabled = false;
        private JsonMap m_map;
        private Dictionary<string, Assembly> m_dependencies;

        public JsonMap Map { get { return this.m_map; } }

        public Plugin()
        {
            this.m_dependencies = new Dictionary<string, Assembly>();
            LoadAssembly("MapRipper.Lib.Ionic.ZLib.dll");
            LoadAssembly("MapRipper.Lib.Newtonsoft.Json.dll");
            LoadAssembly("MapRipper.Lib.MetroFramework.dll");
            LoadAssembly("MapRipper.Lib.MetroFramework.Design.dll");
            LoadAssembly("MapRipper.Lib.MetroFramework.Fonts.dll");

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public string GetAuthor()
        {
            return "Fabian Fischer / ossimc82";
        }

        public string GetName()
        {
            return "Map Ripper";
        }

        public string GetDescription()
        {
            return "You are able to rip maps with this plugin.";
        }

        public string[] GetCommands()
        {
            return new string[4] { "/mapripper enable:disable", "/saveMap", "/saveMap <MapName>", "/mapRipper gui" };
        }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket<HelloPacket>((client, packet) => this.m_map = new JsonMap());
            proxy.HookPacket<MapInfoPacket>((client, packet) => this.m_map.Init(packet.Width, packet.Height, packet.Name));
            proxy.HookPacket<UpdatePacket>(OnUpdatePacket);

            proxy.HookCommand("saveMap", OnSaveMapCommand);
            proxy.HookCommand("mapRipper", OnMapRipperCommand);

            //new Thread(() => new HandleForm(this).ShowDialog()).Start();
        }

        private void OnMapRipperCommand(Client client, string command, string[] args)
        {
            if (args.Length > 0 && args[0] == "enable") _enabled = true;
            if (args.Length > 0 && args[0] == "disable") _enabled = false;
            if (args.Length > 0 && args[0] == "gui") PluginUtils.ShowGUI(new HandleForm(this));
        }

        private void OnSaveMapCommand(Client client, string command, string[] args)
        {
            if (!_enabled) return;
            FilePacket mapData = Packet.Create(PacketType.FILE) as FilePacket;
            mapData.Bytes = Encoding.UTF8.GetBytes(this.m_map.ToJson());
            mapData.Name = args.Length == 0 ? this.m_map.Name : args[0];
            client.SendToClient(mapData);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (this.m_dependencies.ContainsKey(args.Name))
                return this.m_dependencies[args.Name];
            return null;
        }

        private void OnUpdatePacket(Client client, UpdatePacket packet)
        {
            if (_enabled) this.m_map.Update(packet);
        }

        private void LoadAssembly(string path)
        {
            using (var assemblyStream = typeof(Plugin).Assembly.GetManifestResourceStream(path))
            {
                byte[] assemblyData = new byte[assemblyStream.Length];
                assemblyStream.Read(assemblyData, 0, assemblyData.Length);
                var assembly = Assembly.Load(assemblyData);
                this.m_dependencies.Add(assembly.FullName, assembly);
            }
        }
    }
}
