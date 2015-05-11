using MineLib.Core;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Extension.Server
{
    public struct RemoveSelectionPacket : IPacketWithSize
    {
        public byte SelectionID;

        public byte ID { get { return 0x1B; } }
        public short Size { get { return 2; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            SelectionID = reader.ReadByte();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteByte(SelectionID);
            
            return this;
        }
    }
}
