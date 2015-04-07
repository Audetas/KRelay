using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class ReconnectPacket : Packet
    {
        public string Name;
        public string Host;
        public int Port;
        public int GameId;
        public int KeyTime;
        public bool IsFromArena;
        public byte[] Key;     

        public override PacketType Type
        { get { return PacketType.RECONNECT; } }

        public override void Read(PacketReader r)
        {
            Name = r.ReadString();
            Host = r.ReadString();
            Port = r.ReadInt32();
            GameId = r.ReadInt32();
            KeyTime = r.ReadInt32();
            IsFromArena = r.ReadBoolean();
            Key = r.ReadBytes(r.ReadInt16());
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Name);
            w.Write(Host);
            w.Write(Port);
            w.Write(GameId);
            w.Write(KeyTime);
            w.Write(IsFromArena);
            w.Write((short)Key.Length);
            w.Write(Key);
        }
    }
}
