using Lib_K_Relay.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.DataStructures {
	public struct PacketStructure : IDataStructure<byte> {
		internal static Dictionary<byte, PacketStructure> Load(XDocument doc) {
			Dictionary<byte, PacketStructure> map = new Dictionary<byte, PacketStructure>();

			doc.Element("Packets")
				.Elements("Packet")
				.ForEach(packet => {
					PacketStructure p = new PacketStructure(packet);
					map[p.ID] = p;
				});

			map[255] = new PacketStructure {
				ID = 255,
				PacketType = PacketType.UNKNOWN,
				Type = typeof(Packet)
			};

			return map;
		}

		private static Type tPacket = typeof(Packet);
		private static Type[] packetTypes = Assembly.GetAssembly(typeof(Proxy)).GetTypes().Where(t => tPacket.IsAssignableFrom(t)).ToArray();

		/// <summary>
		/// The numerical identifier for this packet
		/// </summary>
		public byte ID { get; private set; }

		public string Name { get { return PacketType.ToString(); } }

		/// <summary>
		/// The type of this packet
		/// </summary>
		public PacketType PacketType;

		/// <summary>
		/// The class this packet can be used as
		/// </summary>
		public Type Type;

		public PacketStructure(XElement packet) {
			ID = (byte)packet.ElemDefault("PacketID", "").ParseInt();
			if (!Enum.TryParse(packet.ElemDefault("PacketName", ""), out PacketType)) {
				PacketType = PacketType.UNKNOWN;
			}

			Type = null;
			foreach (Type pType in packetTypes) {
				PacketType t = (Activator.CreateInstance(pType) as Networking.Packets.Packet).Type;
				if (t == PacketType) {
					Type = pType;
				}
			}
		}

		public override string ToString() {
			return string.Format("Packet: {0} (0x{1:X})", PacketType, ID);
		}
	}
}
