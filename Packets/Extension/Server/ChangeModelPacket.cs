using MineLib.Core;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Extension.Server
{
    public struct ChangeModelPacket : IPacketWithSize
    {
        public byte EntityID;
        public string ModelName;

        public byte ID { get { return 0x1D; } }
        public short Size { get { return 66; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            EntityID = reader.ReadByte();
            ModelName = reader.ReadString();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteByte(EntityID);
            stream.WriteString(ModelName);
            
            return this;
        }
    }
}
