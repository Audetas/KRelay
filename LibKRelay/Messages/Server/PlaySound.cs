using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class PlaySound : Message
    {
        public int OwnerId;
        public int SoundId;

        public override void Read(MessageReader r)
        {
            OwnerId = r.ReadInt32();
            SoundId = r.ReadByte();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(OwnerId);
            w.Write((byte)SoundId);
        }
    }
}
