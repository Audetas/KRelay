using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Lib_K_Relay.Utilities
{
    public static class Serializer
    {
        private static Dictionary<PacketType, Type> PacketTypeTypeMap = new Dictionary<PacketType, Type>();
        private static Dictionary<PacketType, byte> PacketTypeIdMap = new Dictionary<PacketType, byte>();
        private static Dictionary<byte, PacketType> PacketIdTypeMap = new Dictionary<byte, PacketType>();

        public static Dictionary<string, ushort> Tiles = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> Items = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> Objects = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> Enemies = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> CompleteGameData = new Dictionary<string, ushort>();

        public static Dictionary<string, string> Servers = new Dictionary<string, string>();

        #region Packet Serialization
        public static void SerializePacketIds()
        {
            string path = DEBUGGetSolutionRoot() + @"/XML/packets.xml";
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                foreach (XmlNode childNode in document.DocumentElement.ChildNodes)
                {
                    string PacketName = "";
                    byte PacketID = 255;
                    foreach (XmlNode grandChildNode in childNode.ChildNodes)
                        //PacketName and ID here
                        switch (grandChildNode.Name.ToLower())
                        {
                            case "packetname":
                                PacketName = grandChildNode.InnerText;
                                break;
                            case "packetid":
                                PacketID = byte.Parse(grandChildNode.InnerText);
                                break;
                            default:
                                break;
                        }

                    //Hoping that those faggots dont use 255 for packetID
                    if (PacketName != "" && PacketID != 255)
                    {
                        PacketType parsedType;
                        if (Enum.TryParse<PacketType>(PacketName, true, out parsedType))
                        {
                            PacketIdTypeMap.Add(PacketID, parsedType);
                            PacketTypeIdMap.Add(parsedType, PacketID);
                        }
                    }
                }
                Console.WriteLine("[Serializer] Serialized {0} packet ids successfully.", PacketTypeIdMap.Count);
            }
            else throw new FileNotFoundException("Unable to find file.", path);
        }

        public static void SerializePacketTypes()
        {
            
            // Reflect all inheriters of Packet and map them according to their Type member.
            Type tPacket = typeof(Packet);
            Type[] packetTypes = Assembly.GetAssembly(typeof(Proxy)).GetTypes()
                .Where(t => tPacket.IsAssignableFrom(t)).ToArray();

            foreach (Type packetType in packetTypes)
            {
                PacketType t = (Activator.CreateInstance(packetType) as Packet).Type;
                PacketTypeTypeMap.Add(t, packetType);
            }

            Console.WriteLine("[Serializer] Mapped {0} packet structures successfully.", PacketTypeTypeMap.Count);
        }
        #endregion

        #region Object Serialization
        public static void SerializeGameObjects()
        {
            SerializeFromXML("tiles", "Ground", Tiles);
            SerializeFromXML("items", "Object", Items);
            SerializeFromXML("objects", "Object", Objects);
            SerializeFromXML("enemies", "Object", Enemies);
            SerializeFromXML("complete gamedata", "Object", CompleteGameData);
        }

        private static void SerializeFromXML(string fileName, string nodeName, Dictionary<string, ushort> dict)
        {
            string path = DEBUGGetSolutionRoot() + @"/XML/" + fileName + ".xml";
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                foreach (XmlNode childNode in document.DocumentElement.ChildNodes)
                {
                    if (childNode.Name == nodeName)
                    {
                        string objectName = childNode.Attributes.GetNamedItem("id").Value;
                        ushort objectId = Convert.ToUInt16(childNode.Attributes.GetNamedItem("type").Value, 16);
                        if (!dict.ContainsKey(objectName)) dict.Add(objectName, objectId);
                    }
                }
                Console.WriteLine("[Serializer] Serialized {0} {1} successfully.", dict.Count, fileName);
            }
            else throw new FileNotFoundException("Unable to find file.", path);
        }
        #endregion

        #region Server Serialization
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

        public static void SerializeServers()
        {
            if (Servers.Count > 0) return;

            string CharList = "http://realmofthemadgodhrd.appspot.com/char/list";
            XmlTextReader reader = new XmlTextReader(CharList);
            while (reader.ReadToFollowing("Server"))
            {
                reader.ReadToFollowing("Name");
                reader.Read();
                string name = reader.Value;
                reader.ReadToFollowing("DNS");
                reader.Read();
                string dns = reader.Value;
                Servers.Add(name, dns);
            }
            Console.WriteLine("[Serializer] Serialized {0} servers successfully.", Servers.Count);
        }
        #endregion

        #region Helpers
        public static PacketType GetPacketPacketType(byte id)
        {
            if (PacketIdTypeMap.ContainsKey(id)) return PacketIdTypeMap[id];
            else return PacketType.UNKNOWN;
        }

        public static byte GetPacketId(PacketType type)
        {
            if (PacketTypeIdMap.ContainsKey(type)) return PacketTypeIdMap[type];
            else return 255;
        }

        public static Type GetPacketType(PacketType type)
        {
            if (PacketTypeTypeMap.ContainsKey(type)) return PacketTypeTypeMap[type];
            else return typeof(Packet);
        }

        public static string GetServerByFullName(string name)
        {
            if (Servers.ContainsKey(name))
                return Servers[name];
            else
                return "";
        }

        public static string GetServerByShortName(string name)
        {
            if (Servers.ContainsKey(name))
                return GetServerByFullName(Servers[name.ToUpper()]);
            else
                return "";
        }
        #endregion

        public static string DEBUGGetSolutionRoot()
        {
            // This is single handedly the worse piece of code i've ever written.
            DirectoryInfo project = Directory.GetParent(Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()).ToString());
            if (Directory.Exists(project.ToString() + "/XML/"))
                return project.ToString();
            else
                return Application.StartupPath;
        }
    }
}
