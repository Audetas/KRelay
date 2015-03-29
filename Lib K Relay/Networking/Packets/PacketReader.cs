using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets
{
    public class PacketReader : BinaryReader
    {
        public PacketReader(MemoryStream input)
            : base(input, Encoding.UTF8) { }

        public override short ReadInt16()
        {
            return IPAddress.NetworkToHostOrder(base.ReadInt16());
        }

        public override int ReadInt32()
        {
            return IPAddress.NetworkToHostOrder(base.ReadInt32());
        }

        public override float ReadSingle()
        {
            byte[] arr = base.ReadBytes(4);
            Array.Reverse(arr);
            return BitConverter.ToSingle(arr, 0);
        }

        public override string ReadString()
        {
            return Encoding.UTF8.GetString(ReadBytes(ReadInt16()));
        }

        public object Read(string type)
        {
            switch (type.ToLower())
            {
                // Primitives
                case "byte": return ReadByte();
                case "bool": return ReadBoolean();
                case "string": return ReadString();
                case "short": return ReadInt16();
                case "int": return ReadInt32();
                case "float": return ReadSingle();

                // DataObjects
                case "bitmapdata": return new BitmapData().Read(this);
                case "entity": return new Entity().Read(this);
                case "item": return new Item().Read(this);
                case "location": return new Location().Read(this);
                case "locationrecord": return new LocationRecord().Read(this);
                case "slotobject": return new SlotObject().Read(this);
                case "statdata": return new StatData().Read(this);
                case "status": return new Status().Read(this);
                case "tile": return new Tile().Read(this);

                // :(
                default: throw new ArgumentException(type + " is not supported by PacketReader.Read()");
            }
        }

        public IEnumerable ReadArray(string type, int length)
        {
            object[] a = new object[length];
            switch (type.ToLower())
            {
                // Primitives
                case "byte[]": return ReadBytes(length);
                case "bool[]": return a.Select((o, i) => a[i] = ReadBoolean());
                case "string[]": return a.Select((o, i) => a[i] = ReadString());
                case "short[]": return a.Select((o, i) => a[i] = ReadInt16());
                case "int[]": return a.Select((o, i) => a[i] = ReadInt32());
                case "float[]": return a.Select((o, i) => a[i] = ReadSingle());

                // DataObjects
                case "bitmapdata[]": return a.Select((o, i) => a[i] = new BitmapData().Read(this)).ToArray();
                case "entity[]": return a.Select((o, i) => a[i] = new Entity().Read(this)).ToArray();
                case "item[]": return a.Select((o, i) => a[i] = new Item().Read(this)).ToArray();
                case "slotobject[]": return a.Select((o, i) => a[i] = new SlotObject().Read(this)).ToArray();
                case "statdata[]": return a.Select((o, i) => a[i] = new StatData().Read(this)).ToArray();
                case "status[]": return a.Select((o, i) => a[i] = new Status().Read(this)).ToArray();
                case "tile[]": return a.Select((o, i) => a[i] = new Tile().Read(this)).ToArray();

                // :(
                default: throw new ArgumentException(type + " is not supportd by PacketReader.ReadArray()");
            }
        }
    }
}
