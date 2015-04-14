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

/**
 * Known bugs:
 * - Every object is -0.5 moved because of refferences //Clone the class to fix :3
 **/

using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapRipper
{
    public class Plugin : IPlugin
    {
        private JsonMap m_map;
        private Dictionary<string, Assembly> m_dependencies;

        public Plugin()
        {
            this.m_dependencies = new Dictionary<string, Assembly>();
            LoadAssembly("MapRipper.Ionic.ZLib.dll");
            LoadAssembly("MapRipper.Newtonsoft.Json.dll");

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
            return new string[2] { "/saveMap", "/saveMap <MapName>" };
        }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket(PacketType.HELLO, (client, packet) => this.m_map = new JsonMap());
            proxy.HookPacket(PacketType.MAPINFO, (client, packet) => this.m_map.Init((packet as MapInfoPacket).Width, (packet as MapInfoPacket).Height, (packet as MapInfoPacket).Name));
            proxy.HookPacket(PacketType.UPDATE, OnUpdatePacket);

            proxy.HookCommand("saveMap", OnSaveMapCommand);
        }

        private void OnSaveMapCommand(Client client, string command, string[] args)
        {
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

        private void OnUpdatePacket(Client client, Packet packet)
        {
            foreach (var t in (packet as UpdatePacket).Tiles)
                this.m_map.Tiles[t.X][t.Y] = t.Type;

            foreach (var tileDef in (packet as UpdatePacket).NewObjs)
            {
                var def = tileDef;

                if (isMapObject(def.ObjectType))
                {
                    def.Status.Position.X -= 0.5F;
                    def.Status.Position.Y -= 0.5F;

                    int _x = (int)def.Status.Position.X;
                    int _y = (int)def.Status.Position.Y;
                    Array.Resize(ref this.m_map.Entities[_x][_y], this.m_map.Entities[_x][_y].Length + 1);
                    Entity[] arr = this.m_map.Entities[_x][_y];

                    arr[arr.Length - 1] = def;
                }
            }
        }

        private bool isMapObject(short objType)
        {
            return true;// Todo: check if player or pet or all that stuff you dont place on the map with the editor
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
