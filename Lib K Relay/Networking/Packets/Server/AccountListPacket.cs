using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class AccountListPacket : Packet
    {
        public int AccountListId;
        public string[] AccountIds;
        public int LockAction;
        public override PacketType Type
        { get { return PacketType.ACCOUNTLIST; } }

        public override void Read(PacketReader r)
        {
            AccountListId = r.ReadInt32();
            AccountIds = new string[r.ReadUInt16()];
            for (int i = 0; i < AccountIds.Length; i++)
                AccountIds[i] = r.ReadString();
            LockAction = r.ReadInt32();

        }

        public override void Write(PacketWriter w)
        {
            w.Write(AccountListId);
            w.Write((ushort)AccountIds.Length);
            foreach (string i in AccountIds)
                w.Write(i);
            w.Write(LockAction);
        }
    }
}
