using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Lib_K_Relay.Utilities
{
    public class Servers
    {
        private static Dictionary<string, string> Names = new Dictionary<string, string>()
        {
            {"USW", "USWest"},
            {"EUW", "EUWest"},
            {"USNW", "USNorthWest"},
            {"USE", "USEast"},
            {"ASE", "AsiaSouthEast"},
            {"USS", "USSouth"},
            {"USSW", "USSouthWest"},
            {"EUE", "EUEast"},
            {"EUN", "EUNorth"},
            {"EUSW", "EUSouthWest"},
            {"USE3", "USEast3"},
            {"USW2", "USWest2"},
            {"USMW2", "USMidWest2"},
            {"USE2", "USEast2"},
            {"AE", "AsiaEast"},
            {"USS3", "USSouth3"},
            {"EUN2", "EUNorth2"},
            {"EUW2", "EUWest2"},
            {"EUS", "EUSouth"},
            {"USS2", "USSouth2"},
            {"USW3", "USWest3"}
        };

        private static Dictionary<string, string> Hosts = new Dictionary<string,string>();

        public static void parseServers()
        {
            String CharList = "http://realmofthemadgodhrd.appspot.com/char/list";
            XmlTextReader reader = new XmlTextReader(CharList);
            while (reader.ReadToFollowing("Server"))
            {
                reader.ReadToFollowing("Name");
                reader.Read();
                string name = reader.Value;
                reader.ReadToFollowing("DNS");
                reader.Read();
                string dns = reader.Value;
                Hosts.Add(name, dns);
            }
        }

        public static string getServerByFullName(string name)
        {
            if (Hosts.ContainsKey(name))
            {
                return Hosts[name];
            }
            else {
                return "";
            }
        }

        public static string getServerByShortName(string name)
        {
            if (Names.ContainsKey(name))
            {
                return getServerByFullName(Names[name]);
            }
            else
            {
                return "";
            }
        }
    }
}
