using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class AccountList : Message
    {
        public int AccountListId;
        public List<string> AccountIds;
        public int LockAction;

        public override void Read(MessageReader r)
        {
            AccountListId = r.ReadInt32();
            int idCount = r.ReadInt16();
            AccountIds = new List<string>(idCount);
            for (int i = 0; i < idCount; i++)
                AccountIds.Add(r.ReadString());
            LockAction = r.ReadInt32();

        }

        public override void Write(MessageWriter w)
        {
            w.Write(AccountListId);
            w.Write((ushort)AccountIds.Count);
            foreach (string i in AccountIds)
                w.Write(i);
            w.Write(LockAction);
        }
    }
}
