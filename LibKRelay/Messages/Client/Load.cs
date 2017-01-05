using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class Load : Message
    {
        public int CharacterId;
        public bool IsFromArena;

        public override void Read(MessageReader r)
        {
            CharacterId = r.ReadInt32();
            IsFromArena = r.ReadBoolean();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(CharacterId);
            w.Write(IsFromArena);
        }
    }
}
