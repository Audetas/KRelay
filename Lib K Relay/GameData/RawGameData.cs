using Lib_K_Relay.Properties;

namespace Lib_K_Relay.GameData {

	/// <summary>
	/// Provides access to the raw game XML data
	/// </summary>
	public static class RawGameData {

		/// <summary>
		/// The complete XML data for the game
		/// </summary>
		public static string CompleteGamedata {
			get { return Resources.CompleteGamedata; }
		}

		/// <summary>
		/// Enemy XML data
		/// </summary>
		public static string Enemies {
			get { return Resources.Enemies; }
		}

		/// <summary>
		/// Item XML data
		/// </summary>
		public static string Items {
			get { return Resources.Items; }
		}

		/// <summary>
		/// Object XML data
		/// </summary>
		public static string Objects {
			get { return Resources.Objects; }
		}

		/// <summary>
		/// Packet XML data
		/// </summary>
		public static string Packets {
			get { return Resources.Packets; }
		}

		/// <summary>
		/// Tile XML data
		/// </summary>
		public static string Tiles {
			get { return Resources.Tiles; }
		}

	}
}
