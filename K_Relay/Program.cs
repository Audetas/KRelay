using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_Relay
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            EmbeddedAssembly.Load("K_Relay.MetroFramework.dll", "K_Relay.MetroFramework.dll");
            EmbeddedAssembly.Load("K_Relay.MetroFramework.Design.dll", "K_Relay.MetroFramework.Design.dll");
            EmbeddedAssembly.Load("K_Relay.MetroFramework.Fonts.dll", "K_Relay.MetroFramework.Fonts.dll");

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            DoAppSetup();
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }

        static void DoAppSetup()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMainMetro());
        }
    }
}
