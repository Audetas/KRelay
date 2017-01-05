using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class Create : Message
    {
        public ushort ClassType;
        public ushort SkinType;

        public override void Read(MessageReader r)
        {
            ClassType = r.ReadUInt16();
            SkinType = r.ReadUInt16();
        }

        public override void Write(MessageWriter w)
        {
            w.Write((ushort)ClassType);
            w.Write((ushort)SkinType);
        }
    }
}
