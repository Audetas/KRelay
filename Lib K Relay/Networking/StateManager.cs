using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking
{
    public class StateManager
    {
        private Proxy _proxy;
        public void Attach(Proxy proxy)
        {
            _proxy = proxy;
            proxy.HookPacket<UpdatePacket>(OnUpdate);
        }

        private void OnUpdate(Client client, UpdatePacket packet)
        {
            if (client.State.ACCID != null) return;

            State resolvedState = null;

            foreach (State cstate in _proxy.States.Values)
                if (cstate.ACCID == client.PlayerData.AccountId)
                    resolvedState = cstate;

            if (resolvedState == null)
                client.State.ACCID = client.PlayerData.AccountId;
            else
                client.State = resolvedState;
        }
    }
}
