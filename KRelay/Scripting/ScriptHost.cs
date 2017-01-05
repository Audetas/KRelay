using LibKRelay;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KRelay.Scripting
{
    static class ScriptHost
    {
        private static Dictionary<string, Func<string, string>> bindings = new Dictionary<string, Func<string,string>>
        {
            { "CONSTANT", Constant },
            { "DEFINE", Define },
            { "BIND", Bind }
        };

        private static Dictionary<string, string> definitions = new Dictionary<string, string>
        {
            { "$scripts", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Scripting" },
            { "$result", "NULL" },
            { "\\n", "\n" },
            { "\\t", "\t" },
            { "\\,", "," },
            { "\\e", "" }
        };

        private static bool verbose = false;

        public static void Verbose(bool verbosity)
        {
            verbose = verbosity;
        }

        public static string Bind(string operand)
        {
            // Derrive type and method names
            string[] operands = operand.Split(", ");
            string binding = operands[0].ToUpper();

            if (binding == operand)
                throw new Exception("A target method was not specified");
            if (bindings.ContainsKey(binding))
                bindings.Remove(binding);

            string strongName = operands[1];

            if (strongName.StartsWith("@"))
            {
                // Bind script instead of method
                bindings.Add(binding, (s) => LoadAndRun(strongName.Substring(1), true));
                return null;
            }

            int lastIndex = strongName.LastIndexOf('.');
            if (lastIndex != -1)
            {
                string type = new string(strongName.Take(lastIndex).ToArray());
                string method = new string(strongName.Skip(lastIndex + 1).ToArray());

                // If target library was specified, add it to type
                if (operands.Length == 3)
                    type += "," + operands[2];

                // Create and add binding
                bindings.Add(binding, ScriptFFI.CreateInterface(type, method));
            }
            else
                throw new Exception("'" + strongName + "' is not a valid method name");
            return null;
        }

        public static string Define(string operand)
        {
            string name = operand.Split(", ")[0];
            string replacement = operand.Replace(name + ", ", "");
            definitions.AddOrUpdate(name, replacement);
            return null;
        }

        public static string Constant(string operand)
        {
            string name = operand.Split(", ")[0];
            string replacement = operand.Replace(name + ", ", "");
            definitions.AddOrUpdate('$' + name, replacement);
            return null;
        }

        public static string LoadAndRun(string location, bool silent)
        {
            if (!silent)
                ConsoleEx.Ok("Running script: " + location);
            string script = LoadScript(location);
            Run(script);
            return script;
        }

        public static void Run(string script)
        {
            string[] lines = script.Split('\n');
            int lineIndex = -1;

            foreach (string line in lines)
            {
                lineIndex++;
                string final = ApplyMutations(line);
                if (verbose) ConsoleEx.Log(">> " + final);
                if (final.StartsWith("//")) continue;
                if (final.Length < 3) continue;

                int spacerIndex = final.IndexOf(' ');
                string result;
                if (final.Length == spacerIndex || spacerIndex == -1)
                {
                    final = final.ToUpper();
                    result = bindings.TryLookup(final, lineIndex)?.TryInvoke(null, lineIndex);
                }
                else
                {
                    string command = new string(final.Take(spacerIndex).ToArray()).ToUpper();
                    string operand = new string(final.Skip(spacerIndex + 1).ToArray());
                    result = bindings.TryLookup(command, lineIndex)?.TryInvoke(operand, lineIndex);
                }

                if (result != null && result != "NULL")
                {
                    definitions["$result"] = result;
                }
            }
        }

        private static string LoadScript(string location)
        {
            Uri temp;
            if (File.Exists(location))
                return File.ReadAllText(location).Replace("\r", "");
            else if (Uri.TryCreate(location, UriKind.Absolute, out temp))
                using (WebClient wc = new WebClient())
                    return wc.DownloadString(location).Replace("\r", "");

            throw new Exception("The resource at " + location + " was unable to be accessed");
        }

        private static string ApplyMutations(string line)
        {
            string left = line;
            
            foreach (var pair in definitions)
                if (pair.Key.Length <= left.Length)
                    left = left.Replace(pair.Key, pair.Value);

            return left;
        }
    }
}