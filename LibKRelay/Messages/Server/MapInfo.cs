using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class MapInfo : Message
    {
        public int Width;
        public int Height;
        public string Name;
        public string DisplayName;
        public int Difficulty;
        public uint Fp;
        public int Background;
        public bool AllowPlayerTeleport;
        public bool ShowDisplays;
        public List<string> ClientXML;
        public List<string> ExtraXML;

        public override void Read(MessageReader r)
        {
            Width = r.ReadInt32();
            Height = r.ReadInt32();
            Name = r.ReadString();
            DisplayName = r.ReadString();
            Fp = r.ReadUInt32();
            Background = r.ReadInt32();
            Difficulty = r.ReadInt32();
            AllowPlayerTeleport = r.ReadBoolean();
            ShowDisplays = r.ReadBoolean();

            int clientCount = r.ReadInt16();
            ClientXML = new List<string>(clientCount);
            for (int i = 0; i < clientCount; i++)
                ClientXML.Add(r.ReadUTF32());

            int extraCount = r.ReadInt16();
            ExtraXML = new List<string>(extraCount);
            for (int i = 0; i < extraCount; i++)
                ExtraXML.Add(r.ReadUTF32());
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Width);
            w.Write(Height);
            w.Write(Name);
            w.Write(DisplayName);
            w.Write(Fp);
            w.Write(Background);
            w.Write(Difficulty);
            w.Write(AllowPlayerTeleport);
            w.Write(ShowDisplays);
            w.Write((ushort)ClientXML.Count);
            foreach (string i in ClientXML)
                w.WriteUTF32(i);
            w.Write((ushort)ExtraXML.Count);
            foreach (string i in ExtraXML)
                w.WriteUTF32(i);
        }
    }
}
