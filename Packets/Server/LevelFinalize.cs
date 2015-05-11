using MineLib.Core;
using MineLib.Core.Data;
using MineLib.Core.IO;
using ProtocolClassic.Data;

namespace ProtocolClassic.Packets.Server
{
    public struct LevelFinalizePacket : IPacketWithSize
    {
        public Position Coordinates;

        public byte ID { get { return 0x04; } }
        public short Size { get { return 7; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            Coordinates = Position.FromReaderShort(reader);

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            Coordinates.ToStreamShort(stream);
            
            return this;
        }
    }
}
