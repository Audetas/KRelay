using LibKRelay.Net;
using LibKRelay.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    public class Message
    {
        public delegate void GenericMessageHandler<T>(Connection client, T packet) where T : Message;

        public bool Send { get; set; } = true;

        private byte[] bytes;

        private static Type[] typeMap;
        private static Dictionary<Type, byte> idMap;
        private static Dictionary<Type, List<object>> hookMap;

        static Message()
        {
            typeMap = Enumerable.Repeat(typeof(Message), 255).ToArray();
            idMap = new Dictionary<Type, byte>();
            hookMap = new Dictionary<Type, List<object>>();
            foreach (string line in Resources.Packets.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!line.Contains(":")) continue;
                string typeName = line.Split(':')[0];
                byte id = byte.Parse(line.Split(':')[1]);

                Type type = Type.GetType("LibKRelay.Messages.Client." + typeName);
                if (type == null)
                    type = Type.GetType("LibKRelay.Messages.Server." + typeName);
                if (type == null)
                    ConsoleEx.Error("A message type cannot be derrived from: " + typeName);

                typeMap[id] = type;
                idMap.Add(type, id);
            }
        }

        public static byte GetId(Type messageType)
        {
            return idMap[messageType];
        }

        public static Message Create(MessageBuffer buffer)
        {
            using (MemoryStream m = new MemoryStream(buffer.Bytes))
            using (MessageReader r = new MessageReader(m))
            {
                r.ReadUInt32();
                var id = r.ReadByte();
                var message = (Message)Activator.CreateInstance(typeMap[id]);
                message.Read(r);
                return message;
            }
        }

        public static void Hook<T>(GenericMessageHandler<T> callback) where T : Message
        {
            Type type = typeof(T);
            if (!hookMap.ContainsKey(type))
                hookMap.Add(type, new List<object>());
            hookMap[type].Add(callback);
        }

        public static void HookLL<T>(GenericMessageHandler<T> callback) where T : Message
        {
            Type type = typeof(T);
            if (!hookMap.ContainsKey(type))
                hookMap.Add(type, new List<object>());
            hookMap[type].Insert(0, callback);
        }

        public static void Fire(Connection connection, Message message)
        {
            List<object> callbacks;
            if (hookMap.TryGetValue(message.GetType(), out callbacks))
            {
                foreach (object callback in callbacks)
                    (callback as Delegate).TryDynamicInvoke(connection, message);
            }
        }

        public virtual void Read(MessageReader r)
        {
            bytes = r.ReadBytes((int)r.BaseStream.Length - 5);
        }

        public virtual void Write(MessageWriter w)
        {
            w.Write(bytes);
        }

        public override string ToString()
        {
            // Use reflection to get the packet's fields and values so we don't have
            // to formulate a ToString method for every packet type.
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Public |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Instance);

            StringBuilder s = new StringBuilder();
            s.Append(GetType().Name + ":");
            foreach (FieldInfo f in fields)
                s.Append("\n\t" + f.Name + " => " + f.GetValue(this));
            return s.ToString();
        }
    }
}
