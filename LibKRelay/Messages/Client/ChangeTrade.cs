using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class ChangeTrade : Message
    {
        public bool[] Offers;

        public override void Read(MessageReader r)
        {
            Offers = new bool[r.ReadInt16()];
            for (int i = 0; i < Offers.Length; i++)
                Offers[i] = r.ReadBoolean();
        }

        public override void Write(MessageWriter w)
        {
            w.Write((ushort)Offers.Length);
            foreach (bool i in Offers)
                w.Write(i);
        }
    }
}
