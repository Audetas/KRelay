using Lib_K_Relay.GameData.DataStructures;
using Lib_K_Relay.Properties;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData {
	public static class RawGameData {
		public static string CompleteGamedata {
			get { return Resources.CompleteGamedata; }
		}

		public static string Enemies {
			get { return Resources.Enemies; }
		}

		public static string Items {
			get { return Resources.Items; }
		}

		public static string Objects {
			get { return Resources.Objects; }
		}

		public static string Packets {
			get { return Resources.Packets; }
		}

		public static string Tiles {
			get { return Resources.Tiles; }
		}
	}

	public class GameDataMap<IDType, DataType> where DataType : IDataStructure<IDType> {

		public Dictionary<IDType, DataType> Map { get; private set; }

		private GameDataMap() { }

		public GameDataMap(Dictionary<IDType, DataType> map) {
			Map = map;
		}

		public DataType ByID(IDType id) {
			return Map[id];
		}

		public DataType ByName(string name) {
			return Map.First(e => e.Value.Name == name).Value;
		}

		public DataType Match(Func<DataType, bool> f) {
			return Map.First(e => f(e.Value)).Value;
		}
	}

	public class GameData {

		public static GameDataMap<ushort, EnemyStructure> Enemies;
		public static GameDataMap<ushort, ItemStructure> Items;
		public static GameDataMap<ushort, TileStructure> Tiles;
		public static GameDataMap<ushort, ObjectStructure> Objects;
		public static GameDataMap<byte, PacketStructure> Packets;
		public static GameDataMap<string, ServerStructure> Servers;

		public static void Load() {
			Parallel.Invoke(
			() => {
				Enemies = new GameDataMap<ushort, EnemyStructure>(EnemyStructure.Load(XDocument.Parse(RawGameData.Enemies)));
				PluginUtils.Log("GameData", "Mapped {0} enemies.", Enemies.Map.Count);
			},
			() => {
				Items = new GameDataMap<ushort, ItemStructure>(ItemStructure.Load(XDocument.Parse(RawGameData.Items)));
				PluginUtils.Log("GameData", "Mapped {0} items.", Items.Map.Count);
			},
			() => {
				Tiles = new GameDataMap<ushort, TileStructure>(TileStructure.Load(XDocument.Parse(RawGameData.Tiles)));
				PluginUtils.Log("GameData", "Mapped {0} tiles.", Tiles.Map.Count);
			},
			() => {
				Objects = new GameDataMap<ushort, ObjectStructure>(ObjectStructure.Load(XDocument.Parse(RawGameData.Objects)));
				PluginUtils.Log("GameData", "Mapped {0} objects.", Objects.Map.Count);
			},
			() => {
				Packets = new GameDataMap<byte, PacketStructure>(PacketStructure.Load(XDocument.Parse(RawGameData.Packets)));
				PluginUtils.Log("GameData", "Mapped {0} packets.", Packets.Map.Count);
			},
			() => {
				Servers = new GameDataMap<string, ServerStructure>(ServerStructure.Load(XDocument.Load("http://realmofthemadgodhrd.appspot.com/char/list")));
				PluginUtils.Log("GameData", "Mapped {0} servers.", Servers.Map.Count);
			});

			PluginUtils.Log("GameData", "Successfully loaded game data.");
		}

	}
}
