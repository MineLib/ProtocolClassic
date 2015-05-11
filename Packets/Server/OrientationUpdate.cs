using MineLib.Core;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Server
{
    public struct OrientationUpdatePacket : IPacketWithSize
    {
        public sbyte PlayerID;
        public byte Yaw;
        public byte Pitch;

        public byte ID { get { return 0x0B; } }
        public short Size { get { return 4; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            PlayerID = reader.ReadSByte();
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
            stream.WriteByte(Yaw);
            stream.WriteByte(Pitch);
            
            return this;
        }
    }
}
