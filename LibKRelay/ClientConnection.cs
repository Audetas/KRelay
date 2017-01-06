using LibKRelay.Messages;
using LibKRelay.Messages.Client;
using LibKRelay.Messages.Server;
using LibKRelay.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static LibKRelay.ClientListener;

namespace LibKRelay
{
    /// <summary>
    /// Encapsulates an end of a connection.
    /// </summary>
    public class ConnectionInfo
    {
        public TcpClient Connection { get; private set; }
        public NetworkStream Stream { get; private set; }
        public MessageBuffer Buffer { get; private set; }
        public RC4Cipher RecvState { get; private set; }
        public RC4Cipher SendState { get; private set; }

        private object sendLock = new object();

        /// <summary>
        /// Encapsulates an existing TcpClient and creates a buffer and ciphers for it.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        public ConnectionInfo(TcpClient connection, string firstKey, string secondKey)
        {
            Connection = connection;
            Stream = connection.GetStream();
            Buffer = new MessageBuffer();
            RecvState = new RC4Cipher(firstKey);
            SendState = new RC4Cipher(secondKey);
            connection.NoDelay = true;
        }

        /// <summary>
        /// Begins a read on the connection based on the state of the buffer.
        /// </summary>
        /// <param name="callback">Target async callback</param>
        public void BeginRead(AsyncCallback callback)
        {
            Stream.BeginRead(Buffer.Bytes, Buffer.Index, Buffer.BytesRemaining(), callback, this);
        }

        /// <summary>
        /// Ciphers and sends a message via the connection. Thread safe.
        /// </summary>
        /// <param name="message">The message to be sent</param>
        public void Send(Message message)
        {
            lock (sendLock)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (MessageWriter w = new MessageWriter(ms))
                    {
                        w.Write(0x0);
                        w.Write(Message.GetId(message.GetType()));
                        message.Write(w);
                    }

                    byte[] data = ms.ToArray();
                    MessageWriter.BlockCopyInt32(data, data.Length);
                    SendState.Cipher(data);
                    Stream.Write(data, 0, data.Length);
                }
            }
        }
    }

    /// <summary>
    /// Represents the connections between the server and a client
    /// </summary>
    public class Connection
    {
        public event ConnectionHandler Disconnected;
        public MapInfo MapInfo { get; private set; }
        public Dictionary<int, Entity> World { get; private set; }
        public ConnectionInfo Client { get; private set; }
        public ConnectionInfo Server { get; private set; }
        private bool firedDisconnect = false;
        private int objectId = -1;
        private int lastUpdate = 0;
        private int previousTime = 0;

        /// <summary>
        /// Takes an existing client connection and forwards it to the server.
        /// </summary>
        /// <param name="connection">Existing connection</param>
        /// <param name="host">Host to connect to</param>
        public Connection(TcpClient connection, string host)
        {
            Client = new ConnectionInfo(connection, Constants.Key0, Constants.Key1);
            var remoteClient = new TcpClient();
            remoteClient.BeginConnect(host, 2050, Connect, remoteClient);
        }

        /// <summary>
        /// Returns the Entity instance representing the player.
        /// </summary>
        public Entity Self
        {
            get
            {
                if (objectId == -1) return null;
                if (!World.ContainsKey(objectId)) return null;
                return World[objectId];
            }
        }

        /// <summary>
        /// Time since the client's connection began.
        /// </summary>
        public int Time
        {
            get { return previousTime + (Environment.TickCount - lastUpdate); }
        }

        /// <summary>
        /// If both the client and server connection are intact.
        /// </summary>
        public bool Connected
        {
            get
            {
                if (Client == null || Server == null) return false;
                return Client.Connection.Connected && Server.Connection.Connected;
            }
        }

        private void Connect(IAsyncResult ar)
        {
            try
            {
                if (ar.IsCompleted)
                {
                    var remoteClient = (TcpClient)ar.AsyncState;
                    remoteClient.EndConnect(ar);
                    Server = new ConnectionInfo(remoteClient, Constants.Key1, Constants.Key0);
                    Client.BeginRead(Read);
                    Server.BeginRead(Read);
                }
            }
            catch (Exception ex)
            {
                ConsoleEx.Error("Client remote connect failed: " + ex.Message);
                Disconnected?.TryDynamicInvoke(this);
                firedDisconnect = true;
            }
        }

        private void Read(IAsyncResult ar)
        {
            var info = (ConnectionInfo)ar.AsyncState;
            var other = info == Server ? Client : Server;

            try
            {
                if (info.Stream.CanRead)
                {
                    var read = info.Stream.EndRead(ar);
                    info.Buffer.Advance(read);

                    if (read == 0)
                    {
                        Disconnected?.Invoke(this);
                        return;
                    }
                    else if (info.Buffer.Index == 4)
                    {
                        info.Buffer.Resize(IPAddress.NetworkToHostOrder(
                            BitConverter.ToInt32(info.Buffer.Bytes, 0)));
                    }
                    else if (info.Buffer.BytesRemaining() == 0)
                    {
                        info.RecvState.Cipher(info.Buffer.Bytes);
                        Message message = Message.Create(info.Buffer);
                        Process(message);
                        Message.Fire(this, message);
                        if (message.Send) other.Send(message);
                        info.Buffer.Reset();
                    }

                    if (Connected)
                        info.BeginRead(Read);
                }
            }
            catch (Exception ex)
            {
                ConsoleEx.Error("Client read failed: " + ex.Message);
                if (!firedDisconnect)
                {
                    firedDisconnect = true;
                    Disconnected?.TryDynamicInvoke(this);
                }
            }
        }

        private void Process(Message message)
        {
            // Update and store player ObjectId
            if (message is CreateSuccess)
                objectId = (message as CreateSuccess).ObjectId;
            // Store MapInfo
            else if (message is MapInfo)
                MapInfo = message as MapInfo;
            // Update position from PlayerShoot
            else if (message is PlayerShoot)
                Self.Status.Position = (message as PlayerShoot).DerivePlayerPosition();
            // Update position from move
            else if (message is Move)
            {
                Move move = message as Move;
                previousTime = move.Time;
                lastUpdate = Environment.TickCount;
                Self.Status.Position = move.NewPosition;
            }
            // Update world from Update
            else if (message is Update)
            {
                Update update = message as Update;
                foreach (var pair in update.NewObjs)
                    World.AddOrUpdate(pair.Key, pair.Value);
                foreach (int obj in update.Drops)
                    World.Remove(obj);
            }
            // Update world from NewTick
            else if (message is NewTick)
            {
                NewTick newTick = message as NewTick;
                foreach (var pair in newTick.Statuses)
                    World[pair.Key].Parse(pair.Value);
            }
        }

        public override string ToString()
        {
            return string.Format("{ ObjectId: {1}, Entity: {2} }", objectId, Self);
        }
    }
}
