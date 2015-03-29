using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Lib_K_Relay.Networking.Packets
{
    public static class PacketSerializer
    {
        public static Dictionary<PacketType, PacketStructure> PacketStructures = new Dictionary<PacketType, PacketStructure>();
        public static Dictionary<PacketType, byte> PacketTypeIdMap = new Dictionary<PacketType, byte>();
        public static Dictionary<byte, PacketType> PacketIdTypeMap = new Dictionary<byte, PacketType>();

        static PacketSerializer()
        {
            PacketStructures.Add(PacketType.UNKNOWN, new PacketStructure(PacketType.UNKNOWN));
            PacketStructures[PacketType.UNKNOWN].DefineElement("PACKET_DATA", "void");
        }

        public static void SerializePacketsFromXmls(string PacketDefinitionsPath, string PacketIDsPath)
        {
            #region Packet Structures
            if (File.Exists(PacketDefinitionsPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(PacketDefinitionsPath);
                foreach (XmlNode ChildNode in document.DocumentElement.ChildNodes)
                {
                    string PacketName = "";
                    PacketType parsedType;
                    PacketStructure structure;

                    if (ChildNode.FirstChild != null)
                    {
                        if (Enum.TryParse<PacketType>(ChildNode.FirstChild.InnerText, true, out parsedType))
                        {
                            structure = new PacketStructure(parsedType);
                            foreach (XmlNode GrandChildNode in ChildNode.ChildNodes)
                            {
                                switch (GrandChildNode.Name.ToLower())
                                {
                                    case "packetname":
                                        PacketName = GrandChildNode.InnerText;
                                        break;
                                    case "packetelements":
                                        foreach (XmlNode GrandGrandChildNode in GrandChildNode.ChildNodes)
                                            if (GrandGrandChildNode.Name.ToLower() == "packetelement")
                                                structure.DefineElement(GrandGrandChildNode.Attributes[1].InnerText, GrandGrandChildNode.Attributes[0].InnerText);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else
                            structure = new PacketStructure(PacketType.FAILURE);

                        if (structure.Elements() != 0)
                        {
                            PacketStructures.Add(parsedType, structure);
                            Console.WriteLine("[Packet Serializer] Defined packet structure for type {0}.", parsedType);
                        }
                    }
                }
            }
            else
                throw new FileNotFoundException();
            #endregion
            #region Map Packet IDs
            if (File.Exists(PacketIDsPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(PacketIDsPath);
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
                        Console.WriteLine("[Packet Serializer] Registered packet type {0} with id {1}", parsedType, PacketID);
                    }
                }
            }
            else
                throw new FileNotFoundException();
            #endregion
        }

        [Obsolete("This method will be removed soon, please use SerializePacketFromXml() instead")]
        public static void SerializePackets(string directory)
        {
            foreach (string file in Directory.GetFiles(
                directory, "*.txt", SearchOption.AllDirectories))
            {
                string[] filelines = File.ReadAllText(file).Split(
                    new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                string typeName = filelines[0].Split('=')[1];
                byte id = byte.Parse(filelines[1].Split('=')[1]);

                PacketType parsedType;
                if (Enum.TryParse<PacketType>(typeName, true, out parsedType))
                {
                    PacketStructure structure = new PacketStructure(parsedType);
                    foreach (string line in filelines.Skip(2))
                    {
                        string type = line.Split(' ')[0].ToLower();
                        string element = line.Split(' ')[1];

                        structure.DefineElement(element, type);
                    }
                    PacketStructures.Add(parsedType, structure);
                    PacketIdTypeMap.Add(id, parsedType);
                    PacketTypeIdMap.Add(parsedType, id);
                    Console.WriteLine("[Packet Serializer] Registered packet type {0} with id {1}", parsedType, id);
                }
                else
                    Console.WriteLine("[Packet Serializer] {0} is an unknown packet type. Packet definition for it has been skipped.", typeName);
            }
        }

        public static PacketStructure GetStructure(PacketType type)
        {
            PacketStructure structure;

            if (!PacketStructures.TryGetValue(type, out structure))
                return PacketStructures[PacketType.UNKNOWN]; // Return the generic packet structure.
            else
                return structure;
        }

        public static PacketStructure GetStructure(byte id)
        {
            PacketType type;

            if (!PacketIdTypeMap.TryGetValue(id, out type))
                return GetStructure(PacketType.UNKNOWN);
            else
                return GetStructure(type);
        }
    }
}
