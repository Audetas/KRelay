using LibKRelay;
using LibKRelay.Messages;
using LibKRelay.Messages.Client;
using LibKRelay.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glow
{
    public class Glow : Plugin
    {
        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Glow"; }

        public string GetDescription()
        { return "You're so excited about K Relay that you're literally glowing!"; }

        public string[] GetCommands()
        { return new string[] { "/AmISpecial" }; }

        public override void Initialize(ClientListener listener)
        {
            Message.Hook<Update>(OnUpdate);
            Message.Hook<PlayerText>(OnPlayerText);
        }

        private void OnPlayerText(Connection con, PlayerText message)
        {
            if (message.IsCommand("amispecial"))
            {
                Random r = new Random();
                int val = 0;
                for (int i = 0; i < 10; i++)
                {
                    val += r.Next(400000, 723411);
                    con.Client.Send(CreateNotification(
                        con.Self.ObjectId, val, "YOU ARE SPECIAL!"));
                }
            }
        }

        private void OnUpdate(Connection client, Update update)
        {
            Entity player;
            if (update.NewObjs.TryGetValue(client.Self.ObjectId, out player))
            {
                player[StatType.Glowing].IntValue = 100;
            }
        }
    }
}
