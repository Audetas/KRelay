using K_Relay.Util;
using Lib_K_Relay;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_Relay
{
    public partial class FrmMain : Form
    {
        private Proxy _proxy;
        private List<Client> _clients;

        public FrmMain()
        {
            InitializeComponent();

            Config.Default.HookUIEvents(this);
            Console.SetOut(new TextBoxStreamWriter(tbxLog));

            Serializer.SerializeServers();
            Serializer.SerializeGameObjects();
            Serializer.SerializePacketIds();
            Serializer.SerializePacketTypes();

            _clients = new List<Client>();
            _proxy = new Proxy();
            _proxy.ProxyListenStarted += ProxyListenStarted;
            _proxy.ProxyListenStopped += ProxyListenStopped;
            _proxy.ClientConnected += ClientConnected;
            _proxy.ClientDisconnected += ClientDisconnected;

            _proxy.Key0 = Config.Default.RC4Key0;
            _proxy.Key1 = Config.Default.RC4Key1;

            string settingsHost = Serializer.GetServerByShortName(Config.Default.DefaultServerName);
            _proxy.RemoteAddress = settingsHost != "" ? settingsHost : "54.241.208.233";
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            InitPackets();
            InitPlugins();

            if (Config.Default.StartProxyByDefault)
                btnToggleProxy_Click(null, null);
        }

        private void btnToggleProxy_Click(object sender, EventArgs e)
        {
            if (btnToggleProxy.Text == "Start Proxy")
            {
                btnToggleProxy.Text = "Stop Proxy";
                SetStatus("Starting", Color.Black);
                _proxy.Start();
            }
            else
            {
                btnToggleProxy.Text = "Start Proxy";
                SetStatus("Stopping", Color.Black);
                _proxy.Stop();
            }
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.DefaultExt = ".txt";

            if (s.ShowDialog() == DialogResult.OK)
            {
                FileStream logFile = File.Open(s.FileName, FileMode.Create);
                using (StreamWriter sw = new StreamWriter(logFile))
                { sw.Write(tbxLog.Text); }
                logFile.Close();
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            tbxLog.Clear();
        }

        private void tbxLog_TextChanged(object sender, EventArgs e)
        {
            // Remove double newlines
            if (tbxLog.Text.Contains("\n\n")) 
                tbxLog.Text = tbxLog.Text.Replace("\n\n", "\n");
            // Scroll to bottom
            tbxLog.SelectionStart = tbxLog.Text.Length;
            tbxLog.ScrollToCaret();
        }

        private void SetStatus(string status, Color color)
        {
            Invoke(new MethodInvoker(() =>
            {
                lblStatus.ForeColor = color;
                lblStatus.Text = status;
                lblStatus.Left = ClientRectangle.Width - lblStatus.Width;
            }));
        }

        #region Proxy Callbacks
        private void ProxyListenStarted(Proxy p)
        {
            SetStatus("Running", Color.Green);
        }

        private void ProxyListenStopped(Proxy p)
        {
            SetStatus("Stopped", Color.Red);
        }

        private void ClientConnected(Client client)
        {
            _clients.Add(client);
            Invoke(new MethodInvoker(() => lblConnectedClients.Text = "Connected Clients: " + _clients.Count));
        }

        private void ClientDisconnected(Client client)
        {
            _clients.Remove(client);
            Invoke(new MethodInvoker(() => lblConnectedClients.Text = "Connected Clients: " + _clients.Count));
        }
        #endregion
    }
}
