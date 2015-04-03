using K_Relay.Util;
using Lib_K_Relay;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Util;
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
                lblStatus.Text = "Starting";
                _proxy.Start();
            }
            else
            {
                btnToggleProxy.Text = "Start Proxy";
                lblStatus.Text = "Stopping";
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
            // Scroll to bottom
            tbxLog.SelectionStart = tbxLog.Text.Length;
            tbxLog.ScrollToCaret();
        }

        #region Proxy Callbacks
        private void ProxyListenStarted(Proxy p)
        {
            Invoke(new MethodInvoker(() =>
            {
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Running";
            }));
        }

        private void ProxyListenStopped(Proxy p)
        {
            Invoke(new MethodInvoker(() =>
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Stopped";
            }));
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
