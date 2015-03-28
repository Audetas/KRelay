using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking
{
    class PacketBuffer
    {
        private byte[] _buffer;
        private int _index;

        public PacketBuffer()
        {
            Flush();
        }

        public void Resize(int newSize)
        {
            Array.Resize(ref _buffer, newSize);
        }

        public void Advance(int numBytes)
        {
            _index += numBytes;
        }

        public void Flush()
        {
            _buffer = new byte[4];
            _index = 0;
        }

        public int BytesRemaining()
        {
            return _buffer.Length - _index;
        }

        public byte[] Buffer()
        {
            return _buffer;
        }

        public int Length()
        {
            return _buffer.Length;
        }

        public int Index()
        {
            return _index;
        }
    }
}
