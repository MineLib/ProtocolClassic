using MineLib.Core;
using MineLib.Core.Data;
using MineLib.Core.IO;
using ProtocolClassic.Enums;

namespace ProtocolClassic.Packets.Client
{
    public struct SetBlockPacket : IPacketWithSize
    {
        public Position Coordinates;
        public SetBlockMode Mode;
        public byte BlockType;

        public byte ID { get { return 0x05; } }
        public short Size { get { return 9; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            Coordinates = Position.FromReaderShort(reader);
            Mode = (SetBlockMode) reader.ReadByte();
            BlockType = reader.ReadByte();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            Coordinates.ToStreamShort(stream);
            stream.WriteByte((byte) Mode);
            stream.WriteByte(BlockType);
            
            return this;
        }
    }
}
