using System;
using System.Collections.Generic;
using System.Linq;

using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.GameData;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Utilities;
using System.Text;

namespace MapCacher {
	static class Extensions {
		public static T Shift<T>(this List<T> list) {
			if (list.Count < 1) throw new IndexOutOfRangeException("List is empty.");
			T output = list[0];
			list.RemoveAt(0);
			return output;
		}

		public static List<T> Shift<T>(this List<T> list, int count) {
			count = Math.Min(count, list.Count);
			List<T> output = list.GetRange(0, count);
			list.RemoveRange(0, count);
			return output;
		}
	}

	class Map {
		public uint UUID;
		public int Width, Height;
		public ushort[,] Data;

		public Map(MapInfoPacket info) {
			UUID = info.Fp;
			Width = info.Width;
			Height = info.Height;
			Data = new ushort[Width, Height];
		}
	}

	public class MapCacher : IPlugin {
		public static readonly int MaxTilesPerPacket = 256;

		Dictionary<uint, Map> CachedMaps = new Dictionary<uint, Map>();

		Dictionary<Client, Map> CurrentMaps = new Dictionary<Client, Map>();

		Dictionary<Client, List<Tile>> SendQueues = new Dictionary<Client, List<Tile>>();

		public string GetAuthor() {
			return "apemanzilla";
		}

		public string[] GetCommands() {
			return new string[] {
				"/mapcacher clear - clear all cached maps from memory (try this if connection issues are happening)"
			};
		}

		public string GetDescription() {
			return "Caches tile data in memory, and then sends it back to the client when reentering a previous map.";
		}

		public string GetName() {
			return "Map Cacher";
		}

		public void Initialize(Proxy proxy) {
			proxy.HookPacket(PacketType.MAPINFO, OnMapInfo);
			proxy.HookPacket(PacketType.UPDATE, OnUpdate);
		}

		void OnMapInfo(Client client, Packet p) {
			MapInfoPacket mapInfo = p as MapInfoPacket;

			Map map;
			if (CachedMaps.ContainsKey(mapInfo.Fp)) {
				map = CachedMaps[mapInfo.Fp];
				CurrentMaps[client] = map;

				// queue cached tiles
				SendQueues[client] = new List<Tile>();
				for (short x = 0; x < map.Width; x++)
					for (short y = 0; y < map.Height; y++)
						if (map.Data[x, y] != 0)
							SendQueues[client].Add(new Tile { X = x, Y = y, Type = map.Data[x, y] });
#if DEBUG
				PluginUtils.Log("Map Cacher", "Loaded cached map {0}", mapInfo.Name);
#endif
			} else {
				map = new Map(mapInfo);
				CachedMaps[map.UUID] = map;
				CurrentMaps[client] = map;
				SendQueues[client] = new List<Tile>(); // no tiles cached yet

#if DEBUG
				PluginUtils.Log("Map Cacher", "Loaded new map {0}", mapInfo.Name);
#endif
			}
		}

		void OnUpdate(Client client, Packet p) {
			UpdatePacket update = p as UpdatePacket;

			foreach (Tile tile in update.Tiles)
				CurrentMaps[client].Data[tile.X, tile.Y] = tile.Type;

			if (SendQueues[client].Count > 0) {
				List<Tile> newTiles = new List<Tile>(update.Tiles);
				SendQueues[client].Shift(Math.Min(MaxTilesPerPacket, Math.Max(0, MaxTilesPerPacket - newTiles.Count))).ForEach(tile => newTiles.Add(tile));
				update.Tiles = newTiles.ToArray();

				if (SendQueues[client].Count == 0) {
					PluginUtils.Log("Map Cacher", "Sent cached data to {0}", client.PlayerData.Name ?? "[unknown player]");
				}
			}
		}
	}
}