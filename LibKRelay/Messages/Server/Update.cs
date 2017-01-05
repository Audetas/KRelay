using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class Update : Message
    {
        public List<Tile> Tiles;
        public Dictionary<int, Entity> NewObjs;
        public List<int> Drops;

        public override void Read(MessageReader r)
        {
            int tileCount = r.ReadInt16();
            Tiles = new List<Tile>(tileCount);
            for (int i = 0; i < tileCount; i++)
                Tiles.Add(new Tile(r));

            int objCount = r.ReadInt16();
            NewObjs = new Dictionary<int, Entity>(objCount);
            for (int i = 0; i < objCount; i++)
            {
                Entity e = new Entity(r);
                NewObjs.Add(e.Status.ObjectId, e);
            }

            int dropCount = r.ReadInt16();
            Drops = new List<int>(dropCount);
            for (int i = 0; i < dropCount; i++)
                Drops.Add(r.ReadInt32());
        }

        public override void Write(MessageWriter w)
        {
            w.Write((short)Tiles.Count);
            foreach (Tile t in Tiles) t.Write(w);

            w.Write((short)NewObjs.Count);
            foreach (Entity e in NewObjs.Values) e.Write(w);

            w.Write((short)Drops.Count);
            foreach (int i in Drops) w.Write(i);
        }
    }
}
