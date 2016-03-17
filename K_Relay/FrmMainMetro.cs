using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using K_Relay.Util;
using Lib_K_Relay;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Utilities;
using MetroFramework.Forms;
using MetroFramework.Drawing;

namespace K_Relay
{
    public partial class FrmMainMetro : MetroForm
    {
        private Proxy _proxy;
        private List<Client> _clients;

        public FrmMainMetro()
        {
            InitializeComponent();
            tabMain.SelectedIndex = 0;
            Console.SetOut(new MetroTextBoxStreamWriter(tbxLog));
        }


        private async void FrmMainMetro_Load(object sender, EventArgs e)
        {
            Action[] workers =
            {
                Serializer.SerializeServers,
                Serializer.SerializeGameObjects,
                Serializer.SerializePacketIds,
                Serializer.SerializePacketTypes,
                InitPackets,
                InitPlugins,
                InitSettings
            };

            await Task.Run(() =>
            {
                Parallel.ForEach(workers, (worker) =>
                {
                    try
                    {
                        worker.Invoke();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "There was an error while initializing K Relay.\n" +
                            "Please make sure:\n" +
                            "- All files are extracted and in the same directory\n" +
                            "- AntiVirus is not blocking K Relay's connection\n" +
                            "- Another proxy isn't running on your computer\n\n" +
                            "You can try to restart your computer and see if the issue is fixed.\n" +
                            "Additional info is as follows:\n\n" + ex,
                            "K Relay", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(ex.HResult);
                    }
                });
            });

            _clients = new List<Client>();
            _proxy = new Proxy();
            _proxy.ProxyListenStarted += _ => SetStatus("Running", Color.Green);
            _proxy.ProxyListenStopped += _ => SetStatus("Stopped", Color.Red);

            _proxy.ClientConnected += c => _clients.Add(c);
            _proxy.ClientDisconnected += c => _clients.Remove(c);

            PluginUtils.Log("K Relay", "Initialization complete.");

            if (Config.Default.StartProxyByDefault)
                btnToggleProxy_Click(null, null);
        }

        private void FrmMainMetro_Shown(object sender, EventArgs e)
        {
            /*
            InitPackets();
            InitPlugins();
            InitSettings();*/


        }

        private void m_themeManager_OnStyleChanged(object sender, EventArgs e)
        {
            menuInfo.BackColor = menuPlugins.BackColor = menuPackets.BackColor = menuSettings.BackColor = MetroPaint.GetStyleColor(Style);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnToggleProxy_Click(object sender, EventArgs e)
        {
            if (btnToggleProxy.Text == "Start Proxy")
            {
                btnToggleProxy.Text = "Stop Proxy";
                SetStatus("Starting...", Color.Black);

                _proxy.Start();
            }
            else
            {
                btnToggleProxy.Text = "Start Proxy";
                SetStatus("Stopping...", Color.Black);
                _proxy.Stop();
            }
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog()
            {
                DefaultExt = ".txt",
                AddExtension = true,
                FileName = "K_Relay Log",
                SupportMultiDottedExtensions = true,
                Filter = "Text Files (*.txt)|*.txt"
            };

            if (s.ShowDialog() == DialogResult.OK)
            {
                FileStream logFile = File.Open(s.FileName, FileMode.Create);
                using (StreamWriter sw = new StreamWriter(logFile))
                {
                    sw.Write(tbxLog.Text);
                }
                logFile.Close();
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            tbxLog.Clear();
        }

        private void SetStatus(string status, Color color)
        {
            Invoke(new MethodInvoker(() =>
            {
                lblStatus.ForeColor = color;
                lblStatus.Text = status;
            }));
        }
    }
}
