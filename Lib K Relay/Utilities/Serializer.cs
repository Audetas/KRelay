using Lib_K_Relay.Networking.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Lib_K_Relay.Util
{
    public static class Serializer
    {
        private static Dictionary<PacketType, Type> PacketTypeTypeMap = new Dictionary<PacketType, Type>();
        private static Dictionary<PacketType, byte> PacketTypeIdMap = new Dictionary<PacketType, byte>();
        private static Dictionary<byte, PacketType> PacketIdTypeMap = new Dictionary<byte, PacketType>();

        public static Dictionary<string, int> Tiles = new Dictionary<string, int>();
        public static Dictionary<string, int> Items = new Dictionary<string, int>();
        public static Dictionary<string, int> Objects = new Dictionary<string, int>();

        public static void SerializePacketsIds()
        {
            string path = Application.StartupPath + @"\XML\packets.xml";
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
                        Console.WriteLine("[Serializer] Registered packet type {0} with id {1}", parsedType, PacketID);
                    }
                }
            }
            else throw new FileNotFoundException(path);
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
                Console.WriteLine("[Serializer] Mapped structure for {0}.", t);
            }
        }

        public static void SerializeTiles()
        {
            string path = Application.StartupPath + @"\XML\tiles.xml";
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                foreach (XmlNode childNode in document.DocumentElement.ChildNodes)
                {
                    if (childNode.Name == "Ground")
                    {
                        string tileName = childNode.Attributes.GetNamedItem("id").Value;
                        int tileId = Convert.ToInt32(childNode.Attributes.GetNamedItem("type").Value, 16);
                        if (!Tiles.ContainsKey(tileName)) Tiles.Add(tileName, tileId);
                    }
                }
                Console.WriteLine("[Serializer] Serialized {0} tiles successfully.", Tiles.Count);
            }
            else throw new FileNotFoundException(path);
        }

        public static void SerializeItems()
        {
            string path = Application.StartupPath + @"\XML\items.xml";
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                foreach (XmlNode childNode in document.DocumentElement.ChildNodes)
                {
                    if (childNode.Name == "Object")
                    {
                        string itemName = childNode.Attributes.GetNamedItem("id").Value;
                        int itemId = Convert.ToInt32(childNode.Attributes.GetNamedItem("type").Value, 16);
                        if (!Items.ContainsKey(itemName)) Items.Add(itemName, itemId);
                    }
                }
                Console.WriteLine("[Serializer] Serialized {0} items successfully.", Items.Count);
            }
            else throw new FileNotFoundException(path);
        }

        public static void SerializeObjects()
        {
            string path = Application.StartupPath + @"\XML\objects.xml";
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                foreach (XmlNode childNode in document.DocumentElement.ChildNodes)
                {
                    if (childNode.Name == "Object")
                    {
                        string objectName = childNode.Attributes.GetNamedItem("id").Value;
                        int objectId = Convert.ToInt32(childNode.Attributes.GetNamedItem("type").Value, 16);
                        if (!Objects.ContainsKey(objectName)) Objects.Add(objectName, objectId);
                    }
                }
                Console.WriteLine("[Serializer] Serialized {0} objects successfully.", Objects.Count);
            }
            else throw new FileNotFoundException(path);
        }

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
    }
}
