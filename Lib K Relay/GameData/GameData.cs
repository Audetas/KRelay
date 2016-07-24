using Lib_K_Relay.Properties;
using Lib_K_Relay.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using Lib_K_Relay.GameData.ObjectStructures;
using System.Threading;

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
		public static List<Enemy> Enemies = new List<Enemy>();

		public static List<Item> Items = new List<Item>();

		public static List<Tile> Tiles = new List<Tile>();

		public static List<Packet> Packets = new List<Packet>();

		public static List<krObject> Objects = new List<krObject>();

		public static List<Server> Servers = new List<Server>();
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
				.ForEach(enemy => Enemies.Add(new Enemy(enemy)));

			PluginUtils.Log("GameData", "Loaded {0} enemies.", Enemies.Count);
		}

		private static void LoadItems() {
			Items.Clear();

			XDocument.Parse(Items_Raw)
				.Element("Objects")
				.Elements("Object")
				.Where(elem => elem.HasElement("Item"))
				.ForEach(item => Items.Add(new Item(item)));

			PluginUtils.Log("GameData", "Loaded {0} items.", Items.Count);
		}

		private static void LoadTiles() {
			Tiles.Clear();

			XDocument.Parse(Tiles_Raw)
				.Element("GroundTypes")
				.Elements("Ground")
				.ForEach(tile => Tiles.Add(new Tile(tile)));

			PluginUtils.Log("GameData", "Loaded {0} tiles.", Tiles.Count);
		}

		private static void LoadPackets() {
			Packets.Clear();

			XDocument.Parse(Packets_Raw)
				.Element("Packets")
				.Elements("Packet")
				.ForEach(packet => Packets.Add(new Packet(packet)));

			PluginUtils.Log("GameData", "Loaded {0} packets.", Packets.Count);
		}

		private static void LoadObjects() {
			Objects.Clear();

			XDocument.Parse(Objects_Raw)
				.Element("Objects")
				.Elements("Object")
				.ForEach(obj => Objects.Add(new krObject(obj)));

			PluginUtils.Log("GameData", "Loaded {0} misc objects.", Objects.Count);
		}

		private static void LoadServers() {
			Servers.Clear();

			XDocument.Load("http://realmofthemadgodhrd.appspot.com/char/list")
				.Element("Chars")
				.Element("Servers")
				.Elements("Server")
				.ForEach(server => Servers.Add(new Server(server)));

			PluginUtils.Log("GameData", "Loaded {0} servers.", Servers.Count);
		}
	}
}
