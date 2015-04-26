using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.DataObjects
{
    public interface IDataObject : ICloneable
    {
        IDataObject Read(PacketReader r);
        void Write(PacketWriter w);
    }
}
