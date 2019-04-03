﻿using Lib_K_Relay;
using Lib_K_Relay.GameData;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapFilter {
	public class MapFilter : IPlugin {
		public Dictionary<ushort, ushort> TileFilters = new Dictionary<ushort, ushort>();
		public Dictionary<ushort, ushort> ObjectFilters = new Dictionary<ushort, ushort>();

		private string[] SPLIT = new string[] { "=>" };
		private StringSplitOptions SPS = StringSplitOptions.RemoveEmptyEntries;

		public string GetAuthor() { return "KrazyShank / Kronks"; }

		public string GetName() { return "Map Filter"; }

		public string GetDescription() {
			return "Allows you to flag certains tiles or terrain objects to be replaced with something else.\n" +
				   "Warning: Filtering objects with collision can get you disconnected.";
		}

		public string[] GetCommands() { return new string[] { "/mapfilter settings" }; }

		public void Initialize(Proxy proxy) {
			RebuildCache();
			proxy.HookPacket(PacketType.UPDATE, OnUpdate);
			proxy.HookCommand("mapfilter", OnMapFilterCommand);
		}

		public void RebuildCache() {
			TileFilters.Clear();
			ObjectFilters.Clear();
			foreach (string filter in MapFilterConfig.Default.TileFilters) {
				try {
					TileFilters.Add(
						GameData.Tiles.ByName(filter.Split(SPLIT, SPS)[0]).ID,
						GameData.Tiles.ByName(filter.Split(SPLIT, SPS)[1]).ID);

				} catch { }
			}

			foreach (string filter in MapFilterConfig.Default.ObjectFilters) {
				try {
					ObjectFilters.Add(
						GameData.Objects.ByName(filter.Split(SPLIT, SPS)[0]).ID,
						GameData.Objects.ByName(filter.Split(SPLIT, SPS)[1]).ID);
				} catch { }
			}
		}

		private void OnMapFilterCommand(Client client, string command, string[] args) {
			PluginUtils.ShowGUI(new FrmMapFilterSettings(this));
		}

		private void OnUpdate(Client client, Packet packet) {
			if (!MapFilterConfig.Default.Enabled) return;
			UpdatePacket update = (UpdatePacket)packet;

			// New Objects
			foreach (Entity entity in update.NewObjs) {
				if (ObjectFilters.ContainsKey(entity.ObjectType))
					entity.ObjectType = ObjectFilters[entity.ObjectType];
			}

			// New Tiles
			foreach (Tile tile in update.Tiles) {
				if (TileFilters.ContainsKey(tile.Type))
					tile.Type = TileFilters[tile.Type];
			}
		}
	}
}
