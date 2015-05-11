using MineLib.Core;
using MineLib.Core.Data;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Client
{
    public struct PositionAndOrientationPacket : IPacketWithSize
    {
        public byte PlayerID;
        public Vector3 Position;
        public byte Yaw;
        public byte Pitch;

        public byte ID { get { return 0x08; } }
        public short Size { get { return 10; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            PlayerID = reader.ReadByte();
            Position = Vector3.FromReaderShort(reader);
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
            stream.WriteByte(PlayerID);
            Position.ToStreamShort(stream);
            stream.WriteByte(Yaw);
            stream.WriteByte(Pitch);
            
            return this;
        }
    }
}
