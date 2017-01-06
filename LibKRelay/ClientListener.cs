using LibKRelay.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace LibKRelay
{
    /// <summary>
    /// Listens for incomming client connections and handles them.
    /// </summary>
    public class ClientListener
    {
        public delegate void ConnectionHandler(Connection connection);
        public delegate void ListenHandler(ClientListener listener);

        public event ListenHandler ListenStarted;
        public event ListenHandler ListenStopped;
        public event ConnectionHandler ClientConnected;
        public event ConnectionHandler ClientDisconnected;
        public ServerStructure DefaultServer { get; private set; }
        public string ListenAddress { get; private set; }
        public int ListenPort { get; private set; }
        public List<Connection> Connections { get; private set; }

        private TcpListener listener;

        /// <summary>
        /// Initializes a new ClientListener.
        /// </summary>
        public ClientListener()
        {
            Connections = new List<Connection>();
        }

        /// <summary>
        /// Sets the default server for newly connected clients.
        /// </summary>
        public void SetDefaultServer(ServerStructure server)
        {
            DefaultServer = server;
        }

        /// <summary>
        /// Starts a local listener. Stops the previous listener if applicable.
        /// </summary>
        /// <param name="address">Listen address</param>
        /// <param name="port">Listen port</param>
        public void Listen(string address, int port)
        {
            if (DefaultServer == null)
            {
                ConsoleEx.Error("A default server has not yet been specified.");
                return;
            }

            ListenAddress = address;
            ListenPort = port;

            listener?.Stop();
            listener = new TcpListener(IPAddress.Parse(address), port);
            listener.Start();
            listener.BeginAcceptTcpClient(AcceptTcpClient, null);
            ListenStarted?.TryDynamicInvoke(this);
        }

        /// <summary>
        /// Stops the local listener.
        /// </summary>
        public void Stop()
        {
            listener?.Stop();
            ListenStopped?.TryDynamicInvoke(this);
        }

        private void AcceptTcpClient(IAsyncResult ar)
        {
            try
            {
                if (ar.IsCompleted)
                {
                    var accepted = listener.EndAcceptTcpClient(ar);
                    accepted.NoDelay = true;
                    var connection = new Connection(accepted, DefaultServer.Address);
                    connection.Disconnected += (client) =>
                    {
                        Connections.Remove(client);
                        ClientDisconnected?.TryDynamicInvoke(client);
                        ConsoleEx.Error("Client disconnected");
                    };
                    Connections.Add(connection);
                    ConsoleEx.Ok("Client connected");
                    ClientConnected?.TryDynamicInvoke(connection);
                }
            }
            catch (Exception ex)
            {
                ConsoleEx.Error("Client accept failed: " + ex.Message);
            }

            listener?.BeginAcceptTcpClient(AcceptTcpClient, null);
        }
    }
}
