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
        private static Dictionary<byte, PacketType> PacketIdTypeMap = new Dictionary<byte, PacketType>();

        static PacketSerializer()
        {
            PacketStructures.Add(PacketType.UNKNOWN, new PacketStructure(PacketType.UNKNOWN));
            PacketStructures[PacketType.UNKNOWN].DefineElement("PACKET_DATA", "void");
        }

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
