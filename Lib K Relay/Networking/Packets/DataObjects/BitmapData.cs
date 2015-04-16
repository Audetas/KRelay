using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.DataObjects
{
    public class BitmapData : IDataObject
    {
        public int Width;
        public int Height;
        public byte[] Bytes = new byte[0];

        public IDataObject Read(PacketReader r)
        {
            Width = r.ReadInt32();
            Height = r.ReadInt32();
            Bytes = new byte[Width * Height * 4];
            Bytes = r.ReadBytes(Bytes.Length);

            return this;
        }

        public void Write(PacketWriter w)
        {
            w.Write(Width);
            w.Write(Height);
            w.Write(Bytes);
        }


        public override string ToString()
        {
            return "{ Width=" + Width + ", Height=" + Height + " }";
        }
    }
}
