using MineLib.Core;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Server
{
    public struct PingPacket : IPacketWithSize
    {
        public byte ID { get { return 0x01; } }
        public short Size { get { return 1; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            
            return this;
        }
    }
}
