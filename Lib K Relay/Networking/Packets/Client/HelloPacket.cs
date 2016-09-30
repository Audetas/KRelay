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
        public byte[] Key;
        public string MapJSON;
        public string EntryTag;
        public string GameNet;
        public string GameNetUserId;
        public string PlayPlatform;
        public string PlatformToken;
        public string UserToken;
        
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
            Key = (byte[])(Array)r.ReadBytes(r.ReadInt16());
            MapJSON = r.ReadUTF32();
            EntryTag = r.ReadString();
            GameNet = r.ReadString();
            GameNetUserId = r.ReadString();
            PlayPlatform = r.ReadString();
            PlatformToken = r.ReadString();
            UserToken = r.ReadString();
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
            w.WriteUTF32(MapJSON);
            w.Write(EntryTag);
            w.Write(GameNet);
            w.Write(GameNetUserId);
            w.Write(PlayPlatform);
            w.Write(PlatformToken);
            w.Write(UserToken);
        }
    }
}
