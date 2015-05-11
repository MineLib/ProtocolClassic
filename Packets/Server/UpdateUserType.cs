using MineLib.Core;
using MineLib.Core.IO;
using ProtocolClassic.Enums;

namespace ProtocolClassic.Packets.Server
{
    public struct UpdateUserTypePacket : IPacketWithSize
    {
        public UserType UserType;

        public byte ID { get { return 0x0F; } }
        public short Size { get { return 2; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            UserType = (UserType) reader.ReadByte();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteByte((byte) UserType);
            
            return this;
        }
    }
}
