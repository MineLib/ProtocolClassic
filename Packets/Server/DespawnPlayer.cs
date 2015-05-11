using MineLib.Core;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Server
{
    public struct DespawnPlayerPacket : IPacketWithSize
    {
        public sbyte PlayerID;

        public byte ID { get { return 0x0C; } }
        public short Size { get { return 2; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            PlayerID = reader.ReadSByte();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteSByte(PlayerID);
            
            return this;
        }
    }
}
