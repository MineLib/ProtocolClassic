using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MineLib.Core;
using MineLib.Core.IO;

using ProtocolClassic.IO;
using ProtocolClassic.Packets;

namespace ProtocolClassic
{
    public partial class Protocol : IProtocol
    {
        #region Properties

        public string Name { get { return "Classic"; } }
        public string Version { get { return "0.30"; } }

        public ConnectionState State { get; set; }

        public bool Connected { get { return _stream != null && _stream.Connected; } }

        public bool UseLogin { get; private set; }

        // -- Debugging
        public bool SavePackets { get; private set; }

        public List<IPacket> PacketsReceived { get; private set; }
        public List<IPacket> PacketsSended { get; private set; }

        public List<IPacket> LastPackets
        {
            get
            {
                try { return PacketsReceived.GetRange(PacketsReceived.Count - 50, 50); }
                catch { return null; }
            }
        }
        public IPacket LastPacket { get { return PacketsReceived[PacketsReceived.Count - 1]; } }
        // -- Debugging

        #endregion

        private bool UsingExtensions { get; set; }

        private Task _readTask;

        private IMinecraftClient _minecraft;

        private IProtocolStream _stream;


        public IProtocol Initialize(IMinecraftClient client, INetworkTCP tcp, bool debugPackets = false)
        {
            _minecraft = client;
            _stream = new ClassicStream(tcp);
            SavePackets = debugPackets;

            PacketsReceived = new List<IPacket>();
            PacketsSended = new List<IPacket>();

            SendingAsyncHandlers = new Dictionary<Type, Func<ISendingAsyncArgs, Task>>();
            RegisterSupportedSendings();

            return this;
        }
        private async void ReadCycle()
        {
            while (PacketReceiver())
                await Task.Delay(50);
        }
        
        private bool PacketReceiver()
        {
            if (!Connected)
                return false; // -- Terminate cycle

            if (_stream.Available)
            {
                var packetId = _stream.ReadByte();

                // Connection lost
                if (packetId == 255)
                {
                    Disconnect();
                    return false;
                }

                var length = ServerResponseClassic.ServerResponse[packetId]().Size;
                var data = _stream.ReadByteArray(length - 1);

                HandlePacket(packetId, data);
            }

            return true;
        }

        /// <summary>
        /// Packets are handled here.
        /// </summary>
        /// <param name="id">Packet ID</param>
        /// <param name="data">Packet byte[] data</param>
        private void HandlePacket(int id, byte[] data)
        {
            using (var reader = new ClassicDataReader(data))
            {
                if (ServerResponseClassic.ServerResponse[id] == null)
                    return;

                var packet = ServerResponseClassic.ServerResponse[id]().ReadPacket(reader);

                OnPacketHandled(id, packet, null);

                if (SavePackets)
                    PacketsReceived.Add(packet);
            }
        }


        #region Network

        public void Connect(string ip, ushort port)
        {
            if (Connected)
                throw new ProtocolException("Connection error: Already connected to server.");

            // -- Connect to server.
            _stream.Connect(ip, port);

            // -- Begin data reading.
            if (_readTask != null && _readTask.Status == TaskStatus.Running)
                throw new ProtocolException("Connection error: Task already running.");
            else
                _readTask = Task.Factory.StartNew(ReadCycle);
        }

        public void Disconnect()
        {
            if (!Connected)
                throw new ProtocolException("Connection error: Not connected to server.");

            _stream.Disconnect(false);
        }

        public void SendPacket(IPacket packet)
        {
            if (!Connected)
                throw new ProtocolException("Connection error: Not connected to server.");

            _stream.SendPacket(ref packet);

            if (SavePackets)
                PacketsSended.Add(packet);
        }

        public void SendPacket(ref IPacket packet)
        {
            if (!Connected)
                throw new ProtocolException("Connection error: Not connected to server.");

            _stream.SendPacket(ref packet);

            if (SavePackets)
                PacketsSended.Add(packet);
        }


        public async Task ConnectAsync(string ip, ushort port)
        {
            if (Connected)
                throw new ProtocolException("Connection error: Already connected to server.");

            await _stream.ConnectAsync(ip, port);

            if (_readTask != null && _readTask.Status == TaskStatus.Running)
                throw new ProtocolException("Connection error: Task already running.");
            else
                _readTask = Task.Factory.StartNew(ReadCycle);
        }

        public bool DisconnectAsync()
        {
            if (!Connected)
                throw new ProtocolException("Connection error: Not connected to server.");

            return _stream.DisconnectAsync(false);
        }

        public async Task SendPacketAsync(IPacket packet)
        {
            if (!Connected)
                throw new ProtocolException("Connection error: Not connected to server.");

            await _stream.SendPacketAsync(packet);

            if (SavePackets)
                PacketsSended.Add(packet);
        }

        #endregion


        public void Dispose()
        {
            if (_stream != null)
                _stream.Dispose();

            if (PacketsReceived != null)
                PacketsReceived.Clear();

            if (PacketsSended != null)
                PacketsSended.Clear();
        }
    }
}
