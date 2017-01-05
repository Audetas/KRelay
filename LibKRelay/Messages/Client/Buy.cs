using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class Buy : Message
    {
        public int ObjectId;
		public int Quantity;

        public override void Read(MessageReader r)
        {
            ObjectId = r.ReadInt32();
			Quantity = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(ObjectId);
			w.Write(Quantity);
        }
    }
}
