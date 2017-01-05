using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class Text : Message
    {
        public string Name;
        public int ObjectId;
        public int NumStars;
        public byte BubbleTime;
        public string Recipient;
        public string RawText;
        public string CleanText;

        public override void Read(MessageReader r)
        {
            Name = r.ReadString();
            ObjectId = r.ReadInt32();
            NumStars = r.ReadInt32();
            BubbleTime = r.ReadByte();
            Recipient = r.ReadString();
            RawText = r.ReadString();
            CleanText = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Name);
            w.Write(ObjectId);
            w.Write(NumStars);
            w.Write(BubbleTime);
            w.Write(Recipient);
            w.Write(RawText);
            w.Write(CleanText);
        }
    }
}
