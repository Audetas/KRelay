using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets
{
    public class Packet
    {
        public bool Send = true;
        public byte Id;

        private byte[] _data;

        public virtual PacketType Type
        { get { return PacketType.UNKNOWN; } }

        public virtual void Read(PacketReader r)
        {
            _data = r.ReadBytes((int)r.BaseStream.Length - 5); // All of the packet data
        }

        public virtual void Write(PacketWriter w)
        {
            w.Write(_data); // All of the packet data
        }

        public static Packet CreateInstance(PacketType type)
        {
            Packet packet = (Packet)Activator.CreateInstance(
                PacketSerializer.GetPacketType(type));
            packet.Id = PacketSerializer.GetPacketId(type);
            return packet;
        }

        public static Packet CreateInstance(byte[] data)
        {
            using (PacketReader r = new PacketReader(new MemoryStream(data)))
            {
                r.ReadInt32(); // Skip over int length
                byte id = r.ReadByte();
                PacketType packetType = PacketSerializer.GetPacketPacketType(id);
                Type type = PacketSerializer.GetPacketType(packetType);
                // Reflect the type to a new instance and read its data from the PacketReader
                Packet packet = (Packet)Activator.CreateInstance(type);
                packet.Id = id;
                packet.Read(r);
                return packet;
            }
        }

        public override string ToString()
        {
            // Use reflection to get the packet's fields and values so we don't have
            // to formulate a ToString method for every packet type.
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Public |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Instance);

            StringBuilder s = new StringBuilder();
            s.Append(Type + "(" + Id + ") Packet Instance");
            foreach (FieldInfo f in fields)
                s.Append("\n\t" + f.Name + " => " + f.GetValue(this));
            return s.ToString();
        }

        public string ToStructure()
        {
            // Use reflection to build a list of the packet's fields.
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Public |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Instance);

            StringBuilder s = new StringBuilder();
            s.Append(Type + "(" + Id + ") Packet Structure");
            foreach (FieldInfo f in fields)
                s.Append("\n\t" + f.Name + " => " + f.FieldType.Name);
            return s.ToString();
        }
    }
}
