using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets
{
    public class PacketWriter : BinaryWriter
    {
        public PacketWriter(MemoryStream input)
            : base(input) { }

        public override void Write(short value)
        {
            base.Write(IPAddress.NetworkToHostOrder(value));
        }

        public override void Write(int value)
        {
            base.Write(IPAddress.NetworkToHostOrder(value));
        }

        public override void Write(float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Array.Reverse(b);
            base.Write(b);
        }

        public override void Write(string value)
        {
            byte[] data = Encoding.UTF8.GetBytes(value);
            Write((short)data.Length);
            base.Write(data);
        }

        public void WriteT(object value)
        {
            if (value is IEnumerable<object> && (value as IEnumerable<object>).Count() == 0) return;

            // Primitives
            if (value is byte) base.Write((byte)value);
            else if (value is bool) base.Write((bool)value);
            else if (value is string) Write((string)value);
            else if (value is short) Write((short)value);
            else if (value is int) Write((int)value);
            else if (value is float) Write((float)value);
            //else if (value is byte[]) base.Write((byte[])value);
            else if (value is IEnumerable<byte>) base.Write((value as IEnumerable<byte>).ToArray<byte>());
            else if (value is IEnumerable<int>) foreach (int i in (value as IEnumerable<int>)) Write(i);
            else if (value is IEnumerable<string>) foreach (string s in (value as IEnumerable<string>)) Write(s);

            // DataObjects
            else if (value is IDataObject) (value as IDataObject).Write(this);
            else if (value is IDataObject[])
                foreach (IDataObject i in (value as IDataObject[]))
                    i.Write(this);

            //else if (value is IEnumerable<object>) return;

            else throw new ArgumentException("WriteT does not support writing " + value.GetType() + ".");
        }

        public static void BlockCopyInt32(byte[] data, int int32)
        {
            byte[] lengthBytes = BitConverter.GetBytes(IPAddress.NetworkToHostOrder(int32));
            data[0] = lengthBytes[0];
            data[1] = lengthBytes[1];
            data[2] = lengthBytes[2];
            data[3] = lengthBytes[3];
        }
    }
}
