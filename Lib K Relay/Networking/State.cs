using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking
{
    public class State
    {
        public Client Client;
        public string GUID;
        public string ACCID;

        public byte[] ConRealKey = new byte[0];
        public string ConTargetAddress = Proxy.DefaultServer;
        public int ConTargetPort = 2050;

        public ReconnectPacket LastRealm = null;
        public ReconnectPacket LastDungeon = null;

        public Dictionary<string, dynamic> States;

        public State(Client client, string id)
        {
            Client = client;
            GUID = id;
            States = new Dictionary<string, dynamic>();
        }

        public T Value<T>(string stateName)
        {
            Type type = typeof(T);
            object value = null;

            if (!States.TryGetValue(stateName, out value))
            {
                value = Activator.CreateInstance(type);
                States.Add(stateName, value);
            }
            return (T)value;
        }

        public dynamic this[string stateName]
        {
            get
            {
                dynamic value = null;
                States.TryGetValue(stateName, out value);
                return value;
            }
            set { States[stateName] = value; }
        }
    }
}
