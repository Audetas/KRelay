using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.DataObjects
{
    public class Status : IDataObject
    {
        public int ObjectId;
        public Location Position = new Location();
        public StatData[] Data = new StatData[0];

        public IDataObject Read(PacketReader r)
        {
            ObjectId = r.ReadInt32();
            Position.Read(r);
            Data = new StatData[r.ReadInt16()];

            for (int i = 0; i < Data.Length; i++)
            {
                StatData statData = new StatData();
                statData.Read(r);
                Data[i] = statData;
            }

            return this;
        }

        public void Write(PacketWriter w)
        {
            w.Write(ObjectId);
            Position.Write(w);
            w.Write((short)Data.Length);

            foreach (StatData statdata in Data)
                statdata.Write(w);
        }
    }
}
