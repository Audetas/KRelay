using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class PasswordPromptPacket : Packet
    {
        public const int SIGN_IN = 2;
        public const int SEND_EMAIL_AND_SIGN_IN = 3;
        public const int REGISTER = 4;

        public int CleanPasswordStatus ;
        public override PacketType Type
        { get { return PacketType.PASSWORDPROMPT; } }

        public override void Read(PacketReader r)
        {
            CleanPasswordStatus = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(CleanPasswordStatus);
        }
    }
}
