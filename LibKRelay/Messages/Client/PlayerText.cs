using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class PlayerText : Message
    {
        public string Text;

        public override void Read(MessageReader r)
        {
            Text = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Text);
        }

        /// <summary>
        /// Checks to see if the PlayerText is a command.
        /// Sets Send to false if it is.
        /// </summary>
        /// <param name="command">Command to compare</param>
        /// <returns>If the PlayerText is a command</returns>
        public bool IsCommand(string command)
        {
            bool result = Text.ToLower().StartsWith('/' + command.ToLower());
            if (result) Send = false;
            return result;
        }

        /// <summary>
        /// </summary>
        public string[] GetArgs()
        {
            // TODO: Implement this better
            return Text.Contains(' ')
                ? Text.Split(' ').Skip(1).ToArray()
                : new string[0];
        }
    }
}
