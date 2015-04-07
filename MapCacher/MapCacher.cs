using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCacher
{
    public class MapCacher : IPlugin
    {
        struct Map
        {
            public string name;
            public int width;
            public int height;

            public Map(string n, int x, int y)
            {
                name = n;
                width = x;
                height = y;
            }
        }
        ushort[,] mapdata;
        Boolean initialized;
        Map map;

        public string GetAuthor()
        {
            return "Knorrex";
        }

        public string GetName()
        {
            return "Map Cacher";
        }

        public string GetDescription()
        {
            return "Caches map tiles while playing and will attempt to recognize previously visited maps.\nWhen a map is recognized it will be sent through UPDATE to the client, to display it.";
        }

        public string[] GetCommands()
        {
            return new string[] { };
        }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket(PacketType.UPDATE, OnUpdate);
            proxy.HookPacket(PacketType.MAPINFO, GetMapInfo);
            proxy.HookPacket(PacketType.CREATESUCCESS, OnEnterMap);
            initialized = false;
        }

        public void OnUpdate(Client client, Packet packet)
        {
            UpdatePacket update = (UpdatePacket)packet;

            foreach (Tile tile in update.Tiles)
            {
                mapdata[tile.Y, tile.X] = (ushort)tile.Type;
            }
        }

        public void GetMapInfo(Client client, Packet packet)
        {
            MapInfoPacket mapinfo = (MapInfoPacket)packet;
            map = new Map(mapinfo.Name, mapinfo.Height, mapinfo.Width);
            initialized = false;
        }

        public void OnEnterMap(Client client, Packet packet)
        {
            initialized = true;
            mapdata = new ushort[map.height, map.width];
        }
    }
}
