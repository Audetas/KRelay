using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class NewAbilityUnlockedPacket : Packet
    {
        public Ability AbilityType ;

        public override PacketType Type
        { get { return PacketType.NEWABILITYUNLOCKED; } }

        public override void Read(PacketReader r)
        {
            AbilityType = (Ability)r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write((int)Type);
        }
    }
}
