using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class UpdatePacket : Packet
    {
        public Tile[] Tiles;
        public Entity[] NewObjs;
        public int[] Drops;

        public override PacketType Type
        { get { return PacketType.UPDATE; } }

        public override void Read(PacketReader r)
        {
            Tiles = new Tile[r.ReadInt16()];
            for (int i = 0; i < Tiles.Length; i++)
                Tiles[i] = (Tile)new Tile().Read(r);

            NewObjs = new Entity[r.ReadInt16()];
            for (int i = 0; i < NewObjs.Length; i++)
                NewObjs[i] = (Entity)new Entity().Read(r);

            Drops = new int[r.ReadInt16()];
            for (int i = 0; i < Drops.Length; i++)
                Drops[i] = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write((short)Tiles.Length);
            foreach (Tile t in Tiles) t.Write(w);

            w.Write((short)NewObjs.Length);
            foreach (Entity e in NewObjs) e.Write(w);

            w.Write((short)Drops.Length);
            foreach (int i in Drops) w.Write(i);
        }
    }
}
