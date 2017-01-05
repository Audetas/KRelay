using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class Pic : Message
    {
        public BitmapData BitmapData;

        public override void Read(MessageReader r)
        {
            BitmapData = new BitmapData(r);
        }

        public override void Write(MessageWriter w)
        {
            BitmapData.Write(w);
        }
    }
}
