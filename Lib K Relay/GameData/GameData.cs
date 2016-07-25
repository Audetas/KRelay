using Lib_K_Relay.GameData.DataStructures;
using Lib_K_Relay.Properties;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData {
	/// <summary>
	/// Provides access to the raw XML game data in the form of strings
	/// </summary>
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

	/// <summary>
	/// Represents a mapping of short identifiers to data structures for a given data type
	/// </summary>
	/// <typeparam name="IDType">The type of the short identifier (e.g. byte, ushort, string)</typeparam>
	/// <typeparam name="DataType">The type of the data structure (e.g. PacketStructure, EnemyStructure, ServerStructure)</typeparam>
	public class GameDataMap<IDType, DataType> where DataType : IDataStructure<IDType> {

		/// <summary>
		/// Map of short id -> data structure
		/// </summary>
		public Dictionary<IDType, DataType> Map { get; private set; }

		private GameDataMap() { }

		public GameDataMap(Dictionary<IDType, DataType> map) {
			Map = map;
		}

		/// <summary>
		/// Selects a data structure from this map by short identifier
		/// </summary>
		/// <param name="id">The short identifier</param>
		/// <returns>The data structure</returns>
		/// <example>GameData.Packets.ByID(255) -> Packet: UNKNOWN (255)</example>
		/// <example>GameData.Servers.ByID("USW") -> Server: USWest/USW</example>
		public DataType ByID(IDType id) {
			return Map[id];
		}

		/// <summary>
		/// Selects a data structure from this map by full identifier (strings only)
		/// </summary>
		/// <param name="name">The string identifier</param>
		/// <returns>The data structure</returns>
		/// <example>GameData.Packets.ByName("UNKNOWN") -> Packet: UNKNOWN(255)</example>
		/// <example>GameData.Servers.ByName("USWest") -> Server: USWest/USW</example>
		public DataType ByName(string name) {
			return Map.First(e => e.Value.Name == name).Value;
		}

		/// <summary>
		/// Selects the first value from this map for which the given function returns true.
		/// </summary>
		/// <param name="f">The expression to evaluate</param>
		/// <returns>The data structure</returns>
		/// <example>GameData.Packets.Match(p => p.Type == typeof(NewTickPacket)) -> NEWTICK (47)</example>
		public DataType Match(Func<DataType, bool> f) {
			return Map.First(e => f(e.Value)).Value;
		}
	}

	public class GameData {

		/// <summary>
		/// Maps enemy data ("type" attribute -> enemy structure)
		/// </summary>
		public static GameDataMap<ushort, EnemyStructure> Enemies;

		/// <summary>
		/// Maps item data ("type" attribute -> item structure)
		/// </summary>
		public static GameDataMap<ushort, ItemStructure> Items;

		/// <summary>
		/// Maps tile data ("type" attribute -> tile structure)
		/// </summary>
		public static GameDataMap<ushort, TileStructure> Tiles;

		/// <summary>
		/// Maps object data ("type" attribute -> object structure)
		/// </summary>
		public static GameDataMap<ushort, ObjectStructure> Objects;

		/// <summary>
		/// Maps packet data (PacketID -> packet structure)
		/// </summary>
		public static GameDataMap<byte, PacketStructure> Packets;

		/// <summary>
		/// Maps server data (Abbreviation -> server structure) (e.g. USW -> USWest)
		/// </summary>
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
