using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Lib_K_Relay.Networking.Packets
{
    public static class PacketSerializer
    {
        private static Dictionary<PacketType, Type> PacketTypeTypeMap = new Dictionary<PacketType, Type>();
        private static Dictionary<PacketType, byte> PacketTypeIdMap = new Dictionary<PacketType, byte>();
        private static Dictionary<byte, PacketType> PacketIdTypeMap = new Dictionary<byte, PacketType>();

        public static void SerializePacketsIds(string xmlPath)
        {
            if (File.Exists(xmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(xmlPath);
                foreach (XmlNode childNode in document.DocumentElement.ChildNodes)
                {
                    string PacketName = "";
                    byte PacketID = 255;
                    foreach (XmlNode grandChildNode in childNode.ChildNodes)
                        //PacketName and ID here
                        switch (grandChildNode.Name.ToLower())
                        {
                            case "packetname":
                                PacketName = grandChildNode.InnerText;
                                break;
                            case "packetid":
                                PacketID = byte.Parse(grandChildNode.InnerText);
                                break;
                            default:
                                break;
                        }

                    //Hoping that those faggots dont use 255 for packetID
                    if (PacketName != "" && PacketID != 255)
                    {
                        PacketType parsedType;
                        if (Enum.TryParse<PacketType>(PacketName, true, out parsedType))
                        {
                            PacketIdTypeMap.Add(PacketID, parsedType);
                            PacketTypeIdMap.Add(parsedType, PacketID);
                        }
                        Console.WriteLine("[Packet Serializer] Registered packet type {0} with id {1}", parsedType, PacketID);
                    }
                }
            }
            else
                throw new FileNotFoundException();
        }

        public static void SerializePacketTypes()
        {
            // Reflect all inheriters of Packet and map them according to their Type member.
            Type tPacket = typeof(Packet);
            Type[] packetTypes = Assembly.GetAssembly(typeof(Proxy)).GetTypes()
                .Where(t => tPacket.IsAssignableFrom(t)).ToArray();

            foreach (Type packetType in packetTypes)
            {
                PacketType t = (Activator.CreateInstance(packetType) as Packet).Type;
                PacketTypeTypeMap.Add(t, packetType);
                Console.WriteLine("[Packet Serializer] Mapped structure for {0}.", t);
            }
        }

        public static PacketType GetPacketPacketType(byte id)
        {
            if (PacketIdTypeMap.ContainsKey(id)) return PacketIdTypeMap[id];
            else return PacketType.UNKNOWN;
        }

        public static byte GetPacketId(PacketType type)
        {
            if (PacketTypeIdMap.ContainsKey(type)) return PacketTypeIdMap[type];
            else return 255;
        }

        public static Type GetPacketType(PacketType type)
        {
            if (PacketTypeTypeMap.ContainsKey(type)) return PacketTypeTypeMap[type];
            else return typeof(Packet);
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
        GLOBAL_NOTIFICATION,
        RESKIN,
        ENTER_ARENA
    }
}
