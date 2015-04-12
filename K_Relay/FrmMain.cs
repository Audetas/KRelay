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
            Console.SetOut(new TextBoxStreamWriter(tbxLog));

            try
            {
                Serializer.SerializeServers();
                Serializer.SerializeGameObjects();
                Serializer.SerializePacketIds();
                Serializer.SerializePacketTypes();
            }
            catch (Exception e)
            {
                MessageBox.Show("Either a required file was not found or we weren't able to contact the RotMG account server.\n" +
                                "Here's more detail: \n" +
                                e.Message + "\n" +
                                "Please ensure you extract all of the files to the same folder!\n" +
                                "K Relay will now exit.", "K Relay", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _clients = new List<Client>();
            _proxy = new Proxy();
            _proxy.ProxyListenStarted += _ => SetStatus("Running", Color.Green);
            _proxy.ProxyListenStopped += _ => SetStatus("Stopped", Color.Red);
            _proxy.ClientConnected += (c) =>    _clients.Add(c);
            _proxy.ClientDisconnected += (c) => _clients.Remove(c);

            _proxy.Key0 = Config.Default.RC4Key0;
            _proxy.Key1 = Config.Default.RC4Key1;

            if (!Serializer.Servers.ContainsKey(Config.Default.DefaultServerName))
                Config.Default.DefaultServerName = "USWest";
            
            _proxy.RemoteAddress = Serializer.Servers[Config.Default.DefaultServerName];
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            InitPackets();
            InitPlugins();
            InitSettings();

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
    }
}
