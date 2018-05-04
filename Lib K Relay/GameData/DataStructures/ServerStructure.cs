using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.DataStructures {
	public struct ServerStructure : IDataStructure<string> {
		internal static Dictionary<string, ServerStructure> Load(XDocument doc) {
			Dictionary<string, ServerStructure> map = new Dictionary<string, ServerStructure>();

			doc.Element("Chars")
				.Element("Servers")
				.Elements("Server")
				.ForEach(server => {
					ServerStructure s = new ServerStructure(server);
					map[s.ID] = s;
				});

			return map;
		}
		public static readonly Dictionary<string, string> abbreviations = new Dictionary<string, string> {
			{ "USWest", "USW" },
			{ "USMidWest", "USMW" },
			{ "EUWest", "EUW" },
			{ "USEast", "USE" },
			{ "AsiaSouthEast", "ASE" },
			{ "USSouth", "USS" },
			{ "USSouthWest", "USSW" },
			{ "EUEast", "EUE" },
			{ "EUSouthWest", "EUSW" },
			{ "USEast3", "USE3" },
			{ "USWest2", "USW2" },
			{ "USMidWest2", "USMW2" },
			{ "USEast2", "USE2" },
			{ "USNorthWest", "USNW" },
			{ "AsiaEast", "AE" },
			{ "USSouth3", "USS3" },
			{ "EUNorth", "EUN" },
			{ "EUNorth2", "EUN2" },
			{ "EUWest2", "EUW2" },
			{ "EUSouth", "EUS" },
			{ "USSouth2", "USS2" },
			{ "USWest3", "USW3" },
			{ "Australia", "AUS" }
		};

		/// <summary>
		/// The complete name of this server
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// The abbreviation of this server
		/// </summary>
		public string Abbreviation;
		public string ID { get { return Abbreviation; } }

		/// <summary>
		/// The IP address of this server
		/// </summary>
		public string Address;

		public ServerStructure(XElement server) {
			Name = server.ElemDefault("Name", "");
			Abbreviation = abbreviations.ContainsKey(Name) ? abbreviations[Name] : "";
            Address = /*Dns.GetHostEntry(*/server.ElemDefault("DNS", "")/*).AddressList[0].ToString()*/;
        }

		public override string ToString() {
			return string.Format("Server: {0}/{1} ({2})", Name, Abbreviation, Address);
		}
	}
}
