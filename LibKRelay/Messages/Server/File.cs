using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class File : Message
    {
        public string Name;
        public byte[] Bytes;

        public override void Read(MessageReader r)
        {
            Name = r.ReadString();
            Bytes = new byte[r.ReadInt32()];
            Bytes = r.ReadBytes(Bytes.Length);
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Name);
            w.Write(Bytes.Length);
            w.Write(Bytes);
        }
    }
}
