using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Crypto
{
    public class RC4
    {
        private readonly static int STATE_LENGTH = 256;

        private byte[] engineState;
        private int x;
        private int y;
        private byte[] workingKey;

        public RC4(byte[] key)
        {
            workingKey = key;
            SetKey(workingKey);
        }

        public RC4(string hexString)
        {
            workingKey = HexStringToBytes(hexString);
            SetKey(workingKey);
        }

        public void Cipher(byte[] packet)
        {
            ProcessBytes(packet, 5, packet.Length - 5, packet, 5);
        }

        public void Reset()
        {
            SetKey(workingKey);
        }

        private void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
        {
            /*
            if ((inOff + length) > input.Length)
                throw new ArgumentException("input buffer too short");

            if ((outOff + length) > output.Length)
                throw new ArgumentException("output buffer too short");
            */
            for (int i = 0; i < length; i++)
            {
                x = (x + 1) & 0xff;
                y = (engineState[x] + y) & 0xff;

                // swap
                byte tmp = engineState[x];
                engineState[x] = engineState[y];
                engineState[y] = tmp;

                // xor
                output[i + outOff] = (byte)(input[i + inOff]
                        ^ engineState[(engineState[x] + engineState[y]) & 0xff]);
            }
        }

        private void SetKey(byte[] keyBytes)
        {
            workingKey = keyBytes;
            x = y = 0;

            if (engineState == null)
                engineState = new byte[STATE_LENGTH];

            // reset the state of the engine
            for (int i = 0; i < STATE_LENGTH; i++)
                engineState[i] = (byte)i;

            int i1 = 0, i2 = 0;

            for (int i = 0; i < STATE_LENGTH; i++)
            {
                i2 = ((keyBytes[i1] & 0xff) + engineState[i] + i2) & 0xff;
                // do the byte-swap inline
                byte tmp = engineState[i];
                engineState[i] = engineState[i2];
                engineState[i2] = tmp;
                i1 = (i1 + 1) % keyBytes.Length;
            }
        }

        public static byte[] HexStringToBytes(string key)
        {
            if (key.Length % 2 != 0)
                throw new ArgumentException("Invalid hex string!");

            byte[] bytes = new byte[key.Length / 2];
            char[] c = key.ToCharArray();
            for (int i = 0; i < c.Length; i += 2)
            {
                StringBuilder sb = new StringBuilder(2).Append(c[i]).Append(c[(i + 1)]);
                int j = Convert.ToInt32(sb.ToString(), 16);
                bytes[(i / 2)] = (byte)j;
            }
            return bytes;
        }
    }
}
