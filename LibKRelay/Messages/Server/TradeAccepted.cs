using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class TradeAccepted : Message
    {
        public bool[] MyOffers;
        public bool[] YourOffers;

        public override void Read(MessageReader r)
        {
            MyOffers = new bool[r.ReadInt16()];
            for (int i = 0; i < MyOffers.Length; i++)
                MyOffers[i] = r.ReadBoolean();

            YourOffers = new bool[r.ReadInt16()];
            for (int i = 0; i < YourOffers.Length; i++)
                YourOffers[i] = r.ReadBoolean();
        }

        public override void Write(MessageWriter w)
        {
            w.Write((ushort)MyOffers.Length);
            foreach (bool i in MyOffers)
                w.Write(i);
            w.Write((ushort)YourOffers.Length);
            foreach (bool i in YourOffers)
                w.Write(i);
        }
    }
}
