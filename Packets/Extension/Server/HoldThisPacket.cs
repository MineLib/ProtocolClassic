using MineLib.Core;
using MineLib.Core.IO;
using ProtocolClassic.Enums;

namespace ProtocolClassic.Packets.Extension.Server
{
    public struct HoldThisPacket : IPacketWithSize
    {
        public byte BlockToHold;
        public PreventChange PreventChange;

        public byte ID { get { return 0x14; } }
        public short Size { get { return 3; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            BlockToHold = reader.ReadByte();
            PreventChange = (PreventChange) reader.ReadByte();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteByte(BlockToHold);
            stream.WriteByte((byte) PreventChange);
            
            return this;
        }
    }
}
