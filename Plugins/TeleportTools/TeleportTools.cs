using LibKRelay;
using LibKRelay.Messages;
using LibKRelay.Messages.Client;
using LibKRelay.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleportTools
{
    class TeleportState
    {
        public int QuestId = -1;
        public Location QuestLocation = null;
    }

    public class TeleportTools : Plugin
    {
        private Dictionary<Connection, TeleportState> _states = new Dictionary<Connection, TeleportState>();
        private short[] _classes;

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Teleport Tools"; }

        public string GetDescription()
        { return "Teleport to the player closest to your current quest using /tq." +
                 "Teleport to a player without typing their full name with /tp"; }

        public string[] GetCommands()
        { return new string[] { "/tq", "/tp <partial name>"}; }

        public override void Initialize(ClientListener listener)
        {
            _classes = (short[])Enum.GetValues(typeof(Classes));

            listener.ClientConnected += (c) => _states.Add(c, new TeleportState());
            listener.ClientDisconnected += (c) => _states.Remove(c);

            Message.Hook<QuestObjId>(OnQuestObjId);
            Message.Hook<PlayerText>(OnPlayerText);
        }

        private void OnPlayerText(Connection con, PlayerText message)
        {
            TeleportState state = _states[con];

            if (message.IsCommand("tq"))
            {
                if (state.QuestId != -1 && state.QuestLocation != null)
                {
                    float minDistance = state.QuestLocation.DistanceSquaredTo(con.Self.Position);
                    int target = con.Self.ObjectId;

                    foreach (var pair in con.World)
                    {
                        if (_classes.Contains(pair.Value.ObjectType))
                        {
                            float distance = pair.Value.Position.DistanceSquaredTo(state.QuestLocation);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                target = pair.Key;
                            }
                        }
                    }

                    if (target != con.Self.ObjectId)
                    {
                        Teleport teleport = new Teleport();
                        teleport.ObjectId = target;
                        con.Server.Send(teleport);
                        con.Client.Send(CreateNotification(
                            con.Self.ObjectId, "Teleported to " + con.World[target][StatType.Name]));
                    }
                    else
                    {
                        con.Client.Send(CreateNotification(
                            con.Self.ObjectId, "You're the closest to your quest!"));
                    }
                }
            }
            else if (message.IsCommand("tp"))
            {
                var args = message.GetArgs();
                if (args.Length == 0) return;

                foreach (var pair in con.World)
                {
                    StatData name = pair.Value[StatType.Name];
                    if (name != null && name.StringValue.Contains(args[0].ToLower()))
                    {
                        Teleport teleport = new Teleport();
                        teleport.ObjectId = pair.Key;
                        con.Server.Send(teleport);
                        con.Client.Send(CreateNotification(
                            con.Self.ObjectId, "Teleported to " + name.StringValue));
                        return;
                    }
                }

                con.Client.Send(CreateNotification(
                    con.Self.ObjectId, "Player with \"" + args[0] + "\" in their name was not found!"));
            }
        }

        private void OnQuestObjId(Connection client, QuestObjId message)
        {
            _states[client].QuestId = message.ObjectId; 
        }
    }
}
