using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    public class BitmapData
    {
        public int Width;
        public int Height;
        public byte[] Bytes;

        public BitmapData(int width, int height, byte[] bytes)
        {
            Width = width;
            Height = height;
            Bytes = bytes;
        }

        public BitmapData(MessageReader r)
        {
            Width = r.ReadInt32();
            Height = r.ReadInt32();
            Bytes = new byte[Width * Height * 4];
            Bytes = r.ReadBytes(Bytes.Length);
        }

        public void Write(MessageWriter w)
        {
            w.Write(Width);
            w.Write(Height);
            w.Write(Bytes);
        }

        public BitmapData Clone()
        {
            var data = new byte[Width * Height * 4];
            Array.Copy(Bytes, data, Bytes.Length);

            return new BitmapData(Width, Height, data);
        }

        public override string ToString()
        {
            return "{ Width=" + Width + ", Height=" + Height + " }";
        }
    }
}
