using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class EditAccountListPacket : Packet
    {
        public int AccountListId;
        public bool Add;
        public int ObjectId;

        public override PacketType Type
        { get { return PacketType.EDITACCOUNTLIST; } }

        public override void Read(PacketReader r)
        {
            AccountListId = r.ReadInt32();
            Add = r.ReadBoolean();
            ObjectId = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(AccountListId);
            w.Write(Add);
            w.Write(ObjectId);
        }
    }
}
