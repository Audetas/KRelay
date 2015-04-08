using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets
{
    public class Packet
    {
        public bool Send = true;
        public byte Id;

        private byte[] _data;

        public virtual PacketType Type
        { get { return PacketType.UNKNOWN; } }

        public virtual void Read(PacketReader r)
        {
            _data = r.ReadBytes((int)r.BaseStream.Length - 5); // All of the packet data
        }

        public virtual void Write(PacketWriter w)
        {
            w.Write(_data); // All of the packet data
        }

        public static Packet Create(PacketType type)
        {
            Packet packet = (Packet)Activator.CreateInstance(
                Serializer.GetPacketType(type));
            packet.Id = Serializer.GetPacketId(type);
            return packet;
        }

        public static Packet Create(byte[] data)
        {
            using (PacketReader r = new PacketReader(new MemoryStream(data)))
            {
                r.ReadInt32(); // Skip over int length
                byte id = r.ReadByte();
                PacketType packetType = Serializer.GetPacketPacketType(id);
                Type type = Serializer.GetPacketType(packetType);
                // Reflect the type to a new instance and read its data from the PacketReader
                Packet packet = (Packet)Activator.CreateInstance(type);
                packet.Id = id;
                packet.Read(r);

                return packet;
            }
        }

        public override string ToString()
        {
            // Use reflection to get the packet's fields and values so we don't have
            // to formulate a ToString method for every packet type.
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Public |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Instance);

            StringBuilder s = new StringBuilder();
            s.Append(Type + "(" + Id + ") Packet Instance");
            foreach (FieldInfo f in fields)
                s.Append("\n\t" + f.Name + " => " + f.GetValue(this));
            return s.ToString();
        }

        public string ToStructure()
        {
            // Use reflection to build a list of the packet's fields.
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Public |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Instance);

            StringBuilder s = new StringBuilder();
            s.Append(Type + " [" + Serializer.GetPacketId(Type) + "] \nPacket Structure:\n{");
            foreach (FieldInfo f in fields)
                s.Append("\n  " + f.Name + " => " + f.FieldType.Name);
            s.Append("\n}");
            return s.ToString();
        }
    }

    public enum PacketType
    {
        UNKNOWN,
        FAILURE,
        CREATESUCCESS,
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
        NEWTICK,
        INVSWAP,
        USEITEM,
        SHOWEFFECT,
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
        USEPORTAL,
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
        GLOBALNOTIFICATION,
        RESKIN,
        ENTERARENA,
        LEAVEARENA,
        PETCOMMAND,
        PETYARDCOMMAND,
        TINKERQUEST,
        VIEWQUESTS,
        ARENADEATH,
        ARENANEXTWAVE,
        HATCHEGG,
        NEWABILITYUNLOCKED,
        PASSWORDPROMPT,
        EVOLVEPET,
        QUESTFETCHRESPONSE,
        REMOVEPET,
        UPDATEPET,
        UPGRADEPETYARDRESULT,
        VERIFYEMAILDIALOG,
        QUESTREDEEMRESPONSE
    }
}
