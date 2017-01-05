using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class EditAccountList : Message
    {
        public int AccountListId;
        public bool Add;
        public int ObjectId;

        public override void Read(MessageReader r)
        {
            AccountListId = r.ReadInt32();
            Add = r.ReadBoolean();
            ObjectId = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(AccountListId);
            w.Write(Add);
            w.Write(ObjectId);
        }
    }
}
