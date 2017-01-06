using LibKRelay;
using LibKRelay.Messages;
using LibKRelay.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameNotifier
{
    public class FameNotifier : Plugin
    {
        private Dictionary<Connection, int> _fame = new Dictionary<Connection, int>();

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Fame Notifer"; }

        public string GetDescription()
        { return "Lets you know when your fame has increased."; }

        public string[] GetCommands()
        { return new string[] { }; }

        public override void Initialize(ClientListener proxy)
        {
            Message.Hook<NewTick>(OnNewTick);
            proxy.ClientConnected += (client) => _fame.Add(client, -1);
            proxy.ClientDisconnected += (client) => _fame.Remove(client);
        }

        private void OnNewTick(Connection con, NewTick newTick)
        {
            int fame = _fame[con];
            _fame[con] = con.Self[StatType.CharacterFame];

            if (fame != -1 && con.Self[StatType.CharacterFame] != fame)
                con.Client.Send(
                    CreateNotification(
                        con.Self.ObjectId, "+" + (con.Self[StatType.CharacterFame] - fame) + " fame!"));
        }
    }
}
