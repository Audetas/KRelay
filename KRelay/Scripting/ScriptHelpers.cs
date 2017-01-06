using LibKRelay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KRelay.Scripting
{
    public static class ScriptHelpers
    {
        #region Engine Helpers
        public static string[] Split(this string str, string splitter)
        {
            return str.Split(new[] { splitter }, StringSplitOptions.None);
        }

        public static void AddOrUpdate(this Dictionary<string, string> dict, string key, string value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else
                dict.Add(key, value);
        }

        public static Func<string, string> TryLookup(this Dictionary<string, Func<string, string>> dict, string key, int lineIndex)
        {
            Func<string, string> result;
            if (dict.TryGetValue(key, out result))
                return result;

            ConsoleEx.Error("At line " + lineIndex + ", unknown command: " + key);
            return null;
        }

        public static string TryInvoke(this Func<string, string> action, string operand, int lineIndex)
        {
            try
            {
                return action.Invoke(operand);
            }
            catch (Exception ex)
            {
                ConsoleEx.Error("At line " + lineIndex + ", an error occurred: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region Script Utility Calls
        public static void ListClients()
        {
            ConsoleEx.Ok("Number of clients connected: " + Program.Listener.Clients.Count);
            int index = 0;
            foreach (Connection connection in Program.Listener.Clients)
            {
                ConsoleEx.Log("\t[" + index + "] " + connection.ToString());
                index++;
            }
        }

        public static void SendToServer(int clientIndex, string type)
        {
            //Packet toSend = Packet.CreateFromUserInput(type);
            //Program.Proxy.Clients[clientIndex].SendToServer(toSend);
            ConsoleEx.Ok(type.ToString() + " was sent to the server for client " + clientIndex);
        }

        public static void SendToClient(int clientIndex, string type)
        {
            //Packet toSend = Packet.CreateFromUserInput(type);
            //Program.Proxy.Clients[clientIndex].SendToClient(toSend);
            ConsoleEx.Ok(type.ToString() + " was sent to client " + clientIndex);
        }

        public static void LogPacket(string type, int amount)
        {
            ConsoleEx.Error("Not yet implemented");
            string amountString = amount == 0 ? "until toggled" : amount.ToString() + " times";
            ConsoleEx.Ok(type.ToString() + " packets will be logged " + amountString);
        }

        public static void BlockPacket(string type, int amount)
        {
            ConsoleEx.Error("Not yet implemented");
            string amountString = amount == 0 ? "until toggled" : amount.ToString() + " times";
            ConsoleEx.Ok(type.ToString() + " packets will be blocked " + amountString);
        }

        public static void __Builtin_AsyncRun(string value)
        {
            Task.Run(() => ScriptHost.LoadAndRun(value, true));
        }

        public static void __Builtin_Exit()
        {
            ConsoleEx.Error("Are you sure you want to quit K Relay? (y/n)");
            if (Console.ReadLine().ToLower().StartsWith("y"))
                Environment.Exit(0);
        }
        #endregion
    }
}
