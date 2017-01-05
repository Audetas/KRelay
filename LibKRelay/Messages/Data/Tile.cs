using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    public class Tile
    {
        public short X;
        public short Y;
        public ushort Type;

        public static Tile Parse(string input)
        {
            string[] splits = input.Split(new[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries);
            return new Tile(
                short.Parse(splits[0]),
                short.Parse(splits[1]),
                ushort.Parse(splits[2])
            );
        }

        public Tile(short x, short y, ushort type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public Tile(MessageReader r)
        {
            X = r.ReadInt16();
            Y = r.ReadInt16();
            Type = r.ReadUInt16();
        }

        public void Write(MessageWriter w)
        {
            w.Write(X);
            w.Write(Y);
            w.Write(Type);
        }

        public Tile Clone()
        {
            return new Tile(X, Y, Type);
        }

        public override string ToString()
        {
            return "{ X=" + X + ", Y=" + Y + ", Type=" + Type + " }";
        }
    }
}
