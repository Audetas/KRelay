using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets
{
    public class Packet
    {
        public PacketType Type;
        public PacketStructure Structure;
        public bool Send = true;

        private Dictionary<string, object> _data = new Dictionary<string,object>();
        private bool _isServerPacket;
        private byte _id; // ID is to remain hidden from interfacers

        public Packet(byte[] data, bool isServerPacket)
        {
            _isServerPacket = isServerPacket;

            using (PacketReader r = new PacketReader(new MemoryStream(data)))
            {
                r.ReadInt32();
                _id = r.ReadByte();
                Structure = PacketSerializer.GetStructure(_id);
                Type = Structure.Type;

                for (int i = 0; i < Structure.Elements(); i++)
                {
                    string type = Structure[i];
                    string element = Structure.ElementAt(i);

                    if (type == "void") 
                        _data.Add(element, data.Skip(5)); // Skip ID and Length
                    else if (type == "byte[]")
                        _data.Add(element, r.ReadBytes(Convert.ToInt32(this[i-1]))); // Amount of bytes to read is assumed to be the previous element as an integral
                    else if (type.EndsWith("[]"))
                        _data.Add(element, r.ReadArray(type, r.ReadInt16())); // Amount of DataObjects to be read is assumed to be an int16
                    else _data.Add(element, r.Read(type));
                }
            }
        }

        public object this[string element]
        {
            get { return _data[element]; }
            set { _data[element] = value; }
        }

        public object this[int element]
        {
            get { return _data[Structure.ElementAt(element)]; }
            set { _data[Structure.ElementAt(element)] = value; }
        }

        public T Get<T>(string element)
        {
            return (T)this[element];
        }

        public byte[] Data()
        {
            MemoryStream ms = new MemoryStream();
            using (PacketWriter w = new PacketWriter(ms))
            {
                w.Write((int)0); // Make space for length
                w.Write((byte)_id);

                for (int i = 0; i < Structure.Elements(); i++)
                    w.WriteT(this[i]);
            }
            ms.Close();
            return ms.ToArray();
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append((_isServerPacket ? "Server " : "Client ") +  "Packet " + Structure.Type+ "(" + _id + ")\n{\n");

            for (int i = 0; i < Structure.Elements(); i++ )
            {
                s.Append("\t");
                s.Append(Structure.ElementAt(i));
                s.Append("=");
                s.Append(this[i]);
                s.Append("\n");
            }
            s.Append("}");

            return s.ToString();
        }
    }

    public enum PacketType
    {
        UNKNOWN,
        FAILURE,
        CREATE_SUCCESS,
        CREATE,
        PLAYERSHOOT,
        MOVE,
        PLAYERTEXT,
        TEXT,
        SHOOT2,
        DAMAGE,
        UPDATE,
        UPDATEACK,
        NOTIFICATION,
        NEW_TICK,
        INVSWAP,
        USEITEM,
        SHOW_EFFECT,
        HELLO,
        GOTO,
        INVDROP,
        INVRESULT,
        RECONNECT,
        PING,
        PONG,
        MAPINFO,
        LOAD,
        PIC,
        SETCONDITION,
        TELEPORT,
        USEPORTA,
        DEATH,
        BUY,
        BUYRESULT,
        AOE,
        GROUNDDAMAGE,
        PLAYERHIT,
        ENEMYHIT,
        AOEACK,
        SHOOTACK,
        OTHERHIT,
        SQUAREHIT,
        GOTOACK,
        EDITACCOUNTLIST,
        ACCOUNTLIST,
        QUESTOBJID,
        CHOOSENAME,
        NAMERESULT,
        CREATEGUILD,
        CREATEGUILDRESULT,
        GUILDREMOVE,
        GUILDINVITE,
        ALLYSHOOT,
        SHOOT,
        REQUESTTRADE,
        TRADEREQUESTED,
        TRADESTART,
        CHANGETRADE,
        TRADECHANGED,
        ACCEPTTRADE,
        CANCELTRADE,
        TRADEDONE,
        TRADEACCEPTED,
        CLIENTSTAT,
        CHECKCREDITS,
        ESCAPE,
        FILE,
        INVITEDTOGUILD,
        JOINGUILD,
        CHANGEGUILDRANK,
        PLAYSOUND,
        GLOBAL_NOTIFICATION,
        RESKIN
    }
}
