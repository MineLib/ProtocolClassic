using MineLib.Core;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Server
{
    public struct DisconnectPlayerPacket : IPacketWithSize
    {
        public string Reason;

        public byte ID { get { return 0x0E; } }
        public short Size { get { return 65; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            Reason = reader.ReadString();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteString(Reason);
            
            return this;
        }
    }
}
