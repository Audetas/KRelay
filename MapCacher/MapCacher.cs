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

namespace MapCacher {
	class Map {
		public uint fp;
		public int Width, Height;
		public ushort[,] Data;
		public string Name;

		public Map(MapInfoPacket mapInfo) {
			fp = mapInfo.Fp;
			Width = mapInfo.Width;
			Height = mapInfo.Height;
			Data = new ushort[Width, Height];
			Name = mapInfo.Name;
		}

		public void Clear() {
			Data = new ushort[Width, Height];
		}
	}

	public class MapCacher : IPlugin {
		public static readonly int MaxTilesSent = 64;

		Dictionary<Client, List<Tile>> SendQueue = new Dictionary<Client, List<Tile>>();

		Dictionary<uint, Map> CachedMaps = new Dictionary<uint, Map>();

		Dictionary<Client, Map> CurrentMaps = new Dictionary<Client, Map>();

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
			proxy.ClientConnected += OnConnect;
			proxy.ClientDisconnected += OnDisconnect;

			proxy.HookPacket(PacketType.MAPINFO, OnMapInfo);
			proxy.HookPacket(PacketType.UPDATE, OnUpdate);

			proxy.HookCommand("mapcacher", OnCommand);
		}

		void OnCommand(Client client, string command, string[] args) {
			if (args.Length < 1) {
				client.SendToClient(PluginUtils.CreateOryxNotification("Map Cacher", "Usage:"));
				client.SendToClient(PluginUtils.CreateOryxNotification("Map Cacher", "'/mapcacher clear' - clears all currently cached maps."));
				return;
			}
			switch (args[0]) {
				case "clear":
					CachedMaps.ForEach(cached => cached.Value.Clear());
					client.SendToClient(PluginUtils.CreateOryxNotification("Map Cacher", "All maps were cleared."));
					break;
			}
		}

		void OnConnect(Client client) {
			if (SendQueue.ContainsKey(client)) SendQueue.Remove(client);

			SendQueue[client] = null;
		}

		void OnDisconnect(Client client) {
			if (SendQueue.ContainsKey(client)) SendQueue.Remove(client);
			if (CurrentMaps.ContainsKey(client)) CurrentMaps.Remove(client);
		}

		void OnMapInfo(Client client, Packet p) {
			MapInfoPacket mapInfo = p as MapInfoPacket;

			if (CachedMaps.Any(m => m.Value.fp == mapInfo.Fp)) {
				// map exists
				Map map = CachedMaps.First(m => m.Value.fp == mapInfo.Fp).Value;

				CurrentMaps[client] = map;

				PluginUtils.Log("Map Cacher", "Loaded cached map {0} ({1}x{2}, uuid {3})", map.Name, map.Width, map.Height, map.fp);
			} else {
				// map not cached
				Map map = new Map(mapInfo);

				CurrentMaps[client] = map;
				CachedMaps[mapInfo.Fp] = map;
				SendQueue[client] = new List<Tile>();

				PluginUtils.Log("Map Cacher", "Loaded new map {0} ({1}x{2}, uuid {3})", map.Name, map.Width, map.Height, map.fp);
			}
		}

		void OnUpdate(Client client, Packet p) {
			UpdatePacket update = p as UpdatePacket;

			if (CurrentMaps.ContainsKey(client)) {
				Map current = CurrentMaps[client];

				foreach (Tile t in update.Tiles) {
					current.Data[t.X, t.Y] = t.Type;

					if (SendQueue[client] != null) {
						SendQueue[client].Add(t);
					}
				}

				if (SendQueue[client] == null) {
					List<Tile> tiles = new List<Tile>(update.Tiles);

					for (short x = 0; x < current.Width; x++)
						for (short y = 0; y < current.Height; y++)
							if (current.Data[x, y] != 0)
								tiles.Add(new Tile() { X = x, Y = y, Type = current.Data[x, y] });

					SendQueue[client] = tiles;
				}

				if (SendQueue[client].Count > 0) {
					update.Tiles = SendQueue[client].Take(MaxTilesSent).ToArray();
					SendQueue[client].RemoveAll(t => update.Tiles.Contains(t));
				}
			}
		}
	}
}