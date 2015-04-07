using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class HelloPacket : Packet
    {
        public string BuildVersion;
        public int GameId;
        public string GUID;
        public int Random1;
        public string Password;
        public int Random2;
        public string Secret;
        public int KeyTime;
        public sbyte[] Key;
        public string Obf1;
        public string Obf2;
        public string Obf3;
        public string Obf4;
        public string Obf5;
        public string Obf6;

        public byte[] RAW;

        public override PacketType Type
        { get { return PacketType.HELLO; } }

        public override void Read(PacketReader r)
        {
            BuildVersion = r.ReadString();
            GameId = r.ReadInt32();
            GUID = r.ReadString();
            Random1 = r.ReadInt32();
            Password = r.ReadString();
            Random2 = r.ReadInt32();
            Secret = r.ReadString();
            KeyTime = r.ReadInt32();
            Key = (sbyte[])(Array)r.ReadBytes(r.ReadInt16());
            Obf1 = r.ReadUTF32();
            Obf2 = r.ReadString();
            Obf3 = r.ReadString();
            Obf4 = r.ReadString();
            Obf5 = r.ReadString();
            Obf6 = r.ReadString();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(BuildVersion);
            w.Write(GameId);
            w.Write(GUID);
            w.Write(Random1);
            w.Write(Password);
            w.Write(Random2);
            w.Write(Secret);
            w.Write(KeyTime);
            w.Write((short)Key.Length);
            w.Write((byte[])(Array)Key);
            w.WriteUTF32(Obf1);
            w.Write(Obf2);
            w.Write(Obf3);
            w.Write(Obf4);
            w.Write(Obf5);
            w.Write(Obf6);
        }
    }
}
