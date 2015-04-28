using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class MapInfoPacket : Packet
    {
        public int Width;
        public int Height;
        public string Name;
        public string ClientWorldName;
        public int Difficulty;
        public uint Fp;
        public int Background;
        public bool AllowPlayerTeleport;
        public bool ShowDisplays;
        public string[] ClientXML = new String[0];
        public string[] ExtraXML = new String[0];

        public override PacketType Type
        { get { return PacketType.MAPINFO; } }

        public override void Read(PacketReader r)
        {
            Width = r.ReadInt32();
            Height = r.ReadInt32();
            Name = r.ReadString();
            ClientWorldName = r.ReadString();
            Fp = r.ReadUInt32();
            Background = r.ReadInt32();
            Difficulty = r.ReadInt32();
            AllowPlayerTeleport = r.ReadBoolean();
            ShowDisplays = r.ReadBoolean();

            ClientXML = new string[r.ReadInt16()];
            for (int i = 0; i < ClientXML.Length; i++)
                ClientXML[i] = r.ReadUTF32();

            ExtraXML = new string[r.ReadInt16()];
            for (int i = 0; i < ExtraXML.Length; i++)
                ExtraXML[i] = r.ReadUTF32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Width);
            w.Write(Height);
            w.Write(Name);
            w.Write(ClientWorldName);
            w.Write(Fp);
            w.Write(Background);
            w.Write(Difficulty);
            w.Write(AllowPlayerTeleport);
            w.Write(ShowDisplays);
            w.Write((ushort)ClientXML.Length);
            foreach (string i in ClientXML)
                w.WriteUTF32(i);
            w.Write((ushort)ExtraXML.Length);
            foreach (string i in ExtraXML)
                w.WriteUTF32(i);
        }
    }
}
