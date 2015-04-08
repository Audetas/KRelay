using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using Lib_K_Relay;
using Lib_K_Relay.Utilities;
using Lib_K_Relay.Networking;
/*using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
*/

namespace ClientConsoleTest1
{
    class Program
    {
        private static Proxy _proxy;
        private static List<Client> _clients;
        static void Main(string[] args)
        {
            string GUID = GUIDEncrypter.Encrypt("ToxaHD@gmail.com", GUIDEncrypter.serverPublicKey);
            string password = GUIDEncrypter.Encrypt("SwagOverloaded%", GUIDEncrypter.serverPublicKey);

            Serializer.SerializeServers();
            Serializer.SerializeGameObjects();
            Serializer.SerializePacketIds();
            Serializer.SerializePacketTypes();

            _clients = new List<Client>();
            _proxy = new Proxy();

            _proxy.Key0 = "311f80691451c71d09a13a2a6e";
            _proxy.Key1 = "311f80691451c71d09a13a2a6e";

            _proxy.RemoteAddress ="54.241.208.233";
        }
    }

   /* public class SCry
    {
        static string serverPublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDCKFctVrhfF3m2Kes0FBL/JFeOcmNg9eJz8k/hQy1kadD+XFUpluRqa//Uxp2s9W2qE0EoUCu59ugcf/p7lGuL99UoSGmQEynkBvZct+/M40L0E0rZ4BVgzLOJmIbXMp0J4PnPcb6VLZvxazGcmSfjauC7F3yWYqUbZd/HCBtawwIDAQAB";

        static string key = "";

        public SCry()
        {
            newStuff(SCry.createGuestGUID());
        }

        public static string createGuestGUID()
        {
            int timestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Random rnd = new Random();
            double multiplyFactor = rnd.Next(0, 100000000) / 100000000f;
            double random = multiplyFactor * double.MaxValue;
            return SCry.sha1string(timestamp + "" + random + "" + 1).ToUpper();
        }

        public static string createGuestGUID(DateTime customDateTime)
        {
            int timestamp = (int)(customDateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Random rnd = new Random();
            double multiplyFactor = rnd.Next(0, 100000000) / 100000000f;
            double random = multiplyFactor * double.MaxValue;
            return SCry.sha1string(timestamp + "" + random + "" + 1).ToUpper();
        }


        private static string sha1string(string s)
        {
            return SCry.hexString(SCry.sha1(Encoding.UTF8.GetBytes(s)));
        }

        private static string hexString(byte[] buffer)
        {
            char[] hexArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            char[] hexChars = new char[buffer.Length * 2];
            int v;
            for (int j = 0; j < buffer.Length; j++)
            {
                v = buffer[j] & 0xFF;
                hexChars[j * 2] = hexArray[(int)((uint)v >> 4)];
                hexChars[j * 2 + 1] = hexArray[v & 0x0F];
            }
            return new string(hexChars);
        }

        private static byte[] sha1(byte[] buffer)
        {
            return System.Security.Cryptography.SHA1.Create().ComputeHash(buffer);
        }

        //RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
        public void newStuff(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var publicKeyBytes = Convert.FromBase64String(serverPublicKey);


            IBufferedCipher cipher = CipherUtilities.GetCipher("RSA");
            var asymKeyParam = PublicKeyFactory.CreateKey(publicKeyBytes);
            var rsaKeyParameters = (RsaKeyParameters)asymKeyParam;

            // var cipher = CipherUtilities.GetCipher("RSA");
            cipher.Init(true, rsaKeyParameters);
            var processBlock = cipher.DoFinal(bytes);

            string a = Convert.ToBase64String(processBlock);
            //provider = DecodeX509PublicKey(Encoding.UTF8.GetBytes(serverPublicKey));
        }
        /*public static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509key)
        {
            byte[] SeqOID = { 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01 };

            MemoryStream ms = new MemoryStream(x509key);
            BinaryReader reader = new BinaryReader(ms);

            if (reader.ReadByte() == 0x30)
                ReadASNLength(reader); //skip the size
            else
                return null;

            int identifierSize = 0; //total length of Object Identifier section
            if (reader.ReadByte() == 0x30)
                identifierSize = ReadASNLength(reader);
            else
                return null;

            if (reader.ReadByte() == 0x06) //is the next element an object identifier?
            {
                int oidLength = ReadASNLength(reader);
                byte[] oidBytes = new byte[oidLength];
                reader.Read(oidBytes, 0, oidBytes.Length);
                if (oidBytes.SequenceEqual(SeqOID) == false) //is the object identifier rsaEncryption PKCS#1?
                    return null;

                int remainingBytes = identifierSize - 2 - oidBytes.Length;
                reader.ReadBytes(remainingBytes);
            }

            if (reader.ReadByte() == 0x03) //is the next element a bit string?
            {
                ReadASNLength(reader); //skip the size
                reader.ReadByte(); //skip unused bits indicator
                if (reader.ReadByte() == 0x30)
                {
                    ReadASNLength(reader); //skip the size
                    if (reader.ReadByte() == 0x02) //is it an integer?
                    {
                        int modulusSize = ReadASNLength(reader);
                        byte[] modulus = new byte[modulusSize];
                        reader.Read(modulus, 0, modulus.Length);
                        if (modulus[0] == 0x00) //strip off the first byte if it's 0
                        {
                            byte[] tempModulus = new byte[modulus.Length - 1];
                            Array.Copy(modulus, 1, tempModulus, 0, modulus.Length - 1);
                            modulus = tempModulus;
                        }

                        if (reader.ReadByte() == 0x02) //is it an integer?
                        {
                            int exponentSize = ReadASNLength(reader);
                            byte[] exponent = new byte[exponentSize];
                            reader.Read(exponent, 0, exponent.Length);

                            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                            RSAParameters RSAKeyInfo = new RSAParameters();
                            RSAKeyInfo.Modulus = modulus;
                            RSAKeyInfo.Exponent = exponent;
                            RSA.ImportParameters(RSAKeyInfo);
                            return RSA;
                        }
                    }
                }
            }
            return null;
        }

        public static int ReadASNLength(BinaryReader reader)
        {
            //Note: this method only reads lengths up to 4 bytes long as
            //this is satisfactory for the majority of situations.
            int length = reader.ReadByte();
            if ((length & 0x00000080) == 0x00000080) //is the length greater than 1 byte
            {
                int count = length & 0x0000000f;
                byte[] lengthBytes = new byte[4];
                reader.Read(lengthBytes, 4 - count, count);
                Array.Reverse(lengthBytes); //
                length = BitConverter.ToInt32(lengthBytes, 0);
            }
            return length;
        }
        
    }
*/


    public class GUIDEncrypter
    {
        public static String serverPublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDCKFctVrhfF3m2Kes0FBL/JFeOcmNg9eJz8k/hQy1kadD+XFUpluRqa//Uxp2s9W2qE0EoUCu59ugcf/p7lGuL99UoSGmQEynkBvZct+/M40L0E0rZ4BVgzLOJmIbXMp0J4PnPcb6VLZvxazGcmSfjauC7F3yWYqUbZd/HCBtawwIDAQAB\n";

        static GUIDEncrypter()
        {
           /* AsymmetricKeyParameter akp = new AsymmetricKeyParameter(false);
            SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo()
            PublicKeyFactory.CreateKey()*/
        }

        public static string Encrypt(string value, string publicKey)
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            byte[] publicKeyBytes = Convert.FromBase64String(publicKey);
            AsymmetricKeyParameter asymmertringKeyParameter = PublicKeyFactory.CreateKey(publicKeyBytes);
            RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)asymmertringKeyParameter;
            IBufferedCipher cipher = CipherUtilities.GetCipher("RSA");
            cipher.Init(true, rsaKeyParameters);
            byte[] processBlock = cipher.DoFinal(valueBytes);
            return Convert.ToBase64String(processBlock);
        }
    }

}
