using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class BuyResultPacket : Packet
    {
		/*
		UnknownError = -1
		Success = 0
		InvalidCharacter = 1
		ItemNotFound = 2
		NotEnoughGold = 3
		InventoryFull = 4
		TooLowRank = 5
		NotEnoughFame = 6
		PetFeedSuccess = 7
		*/

		public int Result;
        public string Message;
        public override PacketType Type
        { get { return PacketType.BUYRESULT; } }

        public override void Read(PacketReader r)
        {
            Result = r.ReadInt32();
            Message = r.ReadString();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Result);
            w.Write(Message);
        }
    }
}
