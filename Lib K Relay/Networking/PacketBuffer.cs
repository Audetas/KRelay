using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking
{
    class PacketBuffer
    {
        public int Index = 0;
        public byte[] Bytes;

        public PacketBuffer()
        {
            Bytes = new byte[4];
        }

        public void Resize(int newSize)
        {
            if (newSize > 1048576)
                throw new ArgumentException("New buffer size is too large");

            byte[] old = Bytes;
            Bytes = new byte[newSize];
            Bytes[0] = old[0];
            Bytes[1] = old[1];
            Bytes[2] = old[2];
            Bytes[3] = old[3];
        }

        public void Advance(int numBytes)
        {
            Index += numBytes;
        }

        public void Reset()
        {
            Bytes = new byte[4];
            Index = 0;
        }

        public int BytesRemaining()
        {
            return Bytes.Length - Index;
        }

        public void Dispose()
        {
            Bytes = null;
        }
    }
}
