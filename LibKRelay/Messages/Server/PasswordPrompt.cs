using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class PasswordPrompt : Message
    {
        public const int SIGN_IN = 2;
        public const int SEND_EMAIL_AND_SIGN_IN = 3;
        public const int REGISTER = 4;

		public int CleanPasswordStatus;

        public override void Read(MessageReader r)
        {
            CleanPasswordStatus = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(CleanPasswordStatus);
        }
    }
}
