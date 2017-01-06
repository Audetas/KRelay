using KRelay.Scripting;
using LibKRelay;
using LibKRelay.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Jint;
using Jint.Native;

namespace KRelay
{
    class Program
    {
        public static ClientListener Listener { get; private set; }
        public static PluginLoader Loader { get; private set; }

        static void Main(string[] args)
        {
            string startupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string scriptsPath = startupPath + @"\Scripting\";
            string pluginsPath = startupPath + @"\Plugins\";
            string initPath = scriptsPath + "Init.ks";

            /*
            Engine e = new Engine(cfg => cfg.AllowClr(
                typeof(Program).Assembly,
                typeof(ConsoleEx).Assembly));
            e.Execute(File.ReadAllText(scriptsPath + @"\Init.js"));
            while (true)
            {
                JsValue result = e.Execute(Console.ReadLine()).GetCompletionValue();
                if (!result.IsUndefined() && !result.IsNull())
                    Console.WriteLine(result);
            }*/

            if (!Directory.Exists(scriptsPath))
            {
                ConsoleEx.Error("The K Relay 'Scripting' folder is missing. It is required for K Relay to function.");
                ConsoleEx.Error("Please make sure all files are properly extracted to the same location.");
                Console.ReadLine(); return;
            }
            else if (!File.Exists(initPath))
            {
                ConsoleEx.Error("The K Relay initialization script is missing.");
                ConsoleEx.Error("It should be located at: Scripting\\Init.ks");
                ConsoleEx.Error("Please make sure all files are properly extracted to the same location.");
                Console.ReadLine(); return;
            }
            if (!Directory.Exists(pluginsPath))
            {
                ConsoleEx.Warn("The K Relay 'Plugins' folder is missing.");
                ConsoleEx.Warn("No plugins will be loaded!");
                ConsoleEx.Warn("Please make sure all files are properly extracted to the same location.");
            }

            // Get the application-wide loader and listener ready for interaction
            Listener = new ClientListener();
            Loader = new PluginLoader(Listener);

            // Run the initilization script
            ScriptHost.LoadAndRun(startupPath + @"\Scripting\Init.ks", false);

            // Command input loop
            while (true)
            {
                ScriptHost.Run(Console.ReadLine());
                ConsoleEx.Input();
            }
        }

        public static void SetDefaultServer(string name)
        {
            ServerStructure server = ServerStructure.Parse(name);

            if (server == null)
            {
                ConsoleEx.Error("Specified input couldn't be matched to a server name/address/abbreviation.");
            }
            else
            {
                ConsoleEx.Log("Default server set to: " + server.Name);
                Listener.SetDefaultServer(server);
            }
        }

        public static void StartListen(string address, int port)
        {
            ConsoleEx.Log("Listening on " + address + ":" + port);
            Listener.Listen(address, port);
        }

        public static void StopListen()
        {
            ConsoleEx.Log("Stopping local listener");
            Listener.Stop();
        }

        public static void About()
        {
            ConsoleEx.Ok("A man-in-the-middle proxy for Realm of the Mad God. Written by: KrazyShank/Kronks");
        }

        public static void CompareVersion(string currentVersion)
        {
            using (WebClient wc = new WebClient())
            {
                string newVersion = wc.DownloadString("http://static.drips.pw/rotmg/production/current/version.txt");
                if (newVersion != currentVersion)
                {
                    ConsoleEx.Warn("The specified compatible game version is different from the current.");
                    ConsoleEx.Warn("If you are using a new client with K Relay, you might not be able to connect.");
                }
            }
        }
    }
}