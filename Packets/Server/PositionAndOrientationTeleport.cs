using MineLib.Core;
using MineLib.Core.Data;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Server
{
    public struct PositionAndOrientationTeleportPacket : IPacketWithSize
    {
        public sbyte PlayerID;
        public Position Coordinates;
        public byte Yaw;
        public byte Pitch;

        public byte ID { get { return 0x08; } }
        public short Size { get { return 10; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            PlayerID = reader.ReadSByte();
            Coordinates = Position.FromReaderShort(reader);
            Yaw = reader.ReadByte();
            Pitch = reader.ReadByte();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteSByte(PlayerID);
            Coordinates.ToStreamShort(stream);
            stream.WriteByte(Yaw);
            stream.WriteByte(Pitch);
            
            return this;
        }
    }
}
