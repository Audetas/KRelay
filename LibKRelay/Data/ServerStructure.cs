using LibKRelay.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibKRelay.Data
{
    public class ServerStructure
    {
        private static Dictionary<string, ServerStructure> map;
        static ServerStructure()
        {
            map = new Dictionary<string, ServerStructure>();
            XDocument.Parse(Resources.Servers).Element("Chars")
                .Element("Servers")
                .Elements("Server")
                .ForEach(server => {
                    ServerStructure s = new ServerStructure(server);
                    map[s.Name] = s;
                });
        }

        public static ServerStructure ByName(string name)
        {
            foreach (ServerStructure obj in map.Values)
                if (obj.Name == name)
                    return obj;
            return null;
        }

        public static ServerStructure ByAbbreviation(string abbreviation)
        {
            foreach (ServerStructure obj in map.Values)
                if (obj.Abbreviation == abbreviation)
                    return obj;
            return null;
        }

        public static ServerStructure ByAddress(string address)
        {
            foreach (ServerStructure obj in map.Values)
                if (obj.Address == address)
                    return obj;
            return null;
        }

        public static ServerStructure Parse(string text)
        {
            ServerStructure server =     ByName(text);
            if (server == null) server = ByAddress(text);
            if (server == null) server = ByAbbreviation(text);
            return server;
        }

        private static readonly Dictionary<string, string> abbreviations = new Dictionary<string, string> {
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
            { "USWest3", "USW3" }
        };

        /// <summary>
        /// The complete name of this server
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The abbreviation of this server
        /// </summary>
        public string Abbreviation;

        /// <summary>
        /// The IP address of this server
        /// </summary>
        public string Address;

        public ServerStructure(XElement server)
        {
            Name = server.ElemDefault("Name", "");
            Abbreviation = abbreviations.ContainsKey(Name) ? abbreviations[Name] : "";
            Address = Dns.GetHostEntry(server.ElemDefault("DNS", "")).AddressList[0].ToString();
        }

        public override string ToString()
        {
            return string.Format("Server: {0}/{1} ({2})", Name, Abbreviation, Address);
        }
    }
}
