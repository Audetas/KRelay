using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    public struct ARGB
    {
        public byte A;
        public byte B;
        public byte G;
        public byte R;

        public ARGB(uint argb)
        {
            A = (byte)((argb & 0xff000000) >> 24);
            R = (byte)((argb & 0x00ff0000) >> 16);
            G = (byte)((argb & 0x0000ff00) >> 8);
            B = (byte)((argb & 0x000000ff) >> 0);
        }

        public ARGB(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public static ARGB Read(MessageReader r)
        {
            ARGB ret = new ARGB();
            ret.A = r.ReadByte();
            ret.R = r.ReadByte();
            ret.G = r.ReadByte();
            ret.B = r.ReadByte();
            return ret;
        }

        public void Write(MessageWriter w)
        {
            w.Write(A);
            w.Write(R);
            w.Write(G);
            w.Write(B);
        }

        public override string ToString()
        {
            return "{ A=" + A + ", R=" + R + ", G=" + G + ", B=" + B + " }";
        }
    }
}
