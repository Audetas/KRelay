using Lib_K_Relay.Properties;
using Lib_K_Relay.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using Lib_K_Relay.GameData.ObjectStructures;
using System.Threading;
using Lib_K_Relay.Networking.Packets;

namespace Lib_K_Relay.GameData {
	public static class GameData {

		#region Raw data fields
		public static string CompleteGamedata_Raw {
			get { return Resources.CompleteGamedata; }
		}

		public static string Enemies_Raw {
			get { return Resources.Enemies; }
		}

		public static string Items_Raw {
			get { return Resources.Items; }
		}

		public static string Objects_Raw {
			get { return Resources.Objects; }
		}

		public static string Packets_Raw {
			get { return Resources.Packets; }
		}

		public static string Tiles_Raw {
			get { return Resources.Tiles; }
		}
		#endregion

		#region Wrapped data fields
		public static Dictionary<ushort, EnemyStructure> Enemies = new Dictionary<ushort, EnemyStructure>();

		public static Dictionary<ushort, ItemStructure> Items = new Dictionary<ushort, ItemStructure>();

		public static Dictionary<ushort, TileStructure> Tiles = new Dictionary<ushort, TileStructure>();

		public static Dictionary<byte, PacketStructure> Packets = new Dictionary<byte, PacketStructure>();
		public static Dictionary<PacketType, byte> PacketTypeMap = new Dictionary<PacketType, byte>();

		public static Dictionary<ushort, ObjectStructure> Objects = new Dictionary<ushort, ObjectStructure>();

		public static List<ServerStructure> Servers = new List<ServerStructure>();
		#endregion


		public static void Load() {
			Parallel.Invoke(
				LoadEnemies,
				LoadItems,
				LoadTiles,
				LoadPackets,
				LoadObjects,
				LoadServers
			);


			PluginUtils.Log("GameData", "Parsing complete.");
		}

		private static void LoadEnemies() {
			Enemies.Clear();

			XDocument.Parse(Enemies_Raw)
				.Element("Objects")
				.Elements("Object")
				.Where(elem => elem.HasElement("Enemy"))
				.ForEach(enemy => {
					EnemyStructure e = new EnemyStructure(enemy);
					Enemies[e.ID] = e;
				});

			PluginUtils.Log("GameData", "Loaded {0} enemies.", Enemies.Count);
		}

		private static void LoadItems() {
			Items.Clear();

			XDocument.Parse(Items_Raw)
				.Element("Objects")
				.Elements("Object")
				.Where(elem => elem.HasElement("Item"))
				.ForEach(item => {
					ItemStructure i = new ItemStructure(item);
					Items[i.ID] = i;
				});

			PluginUtils.Log("GameData", "Loaded {0} items.", Items.Count);
		}

		private static void LoadTiles() {
			Tiles.Clear();

			XDocument.Parse(Tiles_Raw)
				.Element("GroundTypes")
				.Elements("Ground")
				.ForEach(tile => {
					TileStructure t = new TileStructure(tile);
					Tiles[t.ID] = t;
				});

			PluginUtils.Log("GameData", "Loaded {0} tiles.", Tiles.Count);
		}

		private static void LoadPackets() {
			Packets.Clear();
			PacketTypeMap.Clear();

			XDocument.Parse(Packets_Raw)
				.Element("Packets")
				.Elements("Packet")
				.ForEach(packet => {
					PacketStructure p = new PacketStructure(packet);
					Packets[p.ID] = p;
					PacketTypeMap[p.PacketType] = p.ID;
				});

			PacketTypeMap[PacketType.UNKNOWN] = 255;
			Packets[255] = new PacketStructure {
				ID = 255,
				PacketType = PacketType.UNKNOWN,
				Type = typeof(Packet)
			};

			PluginUtils.Log("GameData", "Loaded {0} packets.", Packets.Count);
		}

		private static void LoadObjects() {
			Objects.Clear();

			XDocument.Parse(Objects_Raw)
				.Element("Objects")
				.Elements("Object")
				.ForEach(obj => {
					ObjectStructure o = new ObjectStructure(obj);
					Objects[o.ID] = o;
				});

			PluginUtils.Log("GameData", "Loaded {0} misc objects.", Objects.Count);
		}

		private static void LoadServers() {
			Servers.Clear();

			XDocument.Load("http://realmofthemadgodhrd.appspot.com/char/list")
				.Element("Chars")
				.Element("Servers")
				.Elements("Server")
				.ForEach(server => Servers.Add(new ServerStructure(server)));

			PluginUtils.Log("GameData", "Loaded {0} servers.", Servers.Count);
		}
	}
}
