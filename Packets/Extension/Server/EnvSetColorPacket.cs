using MineLib.Core;
using MineLib.Core.IO;
using ProtocolClassic.Enums;

namespace ProtocolClassic.Packets.Extension.Server
{
    public struct EnvSetColorPacket : IPacketWithSize
    {
        public EnvironmentalVariable Variable;
        public short Red;
        public short Green;
        public short Blue;

        public byte ID { get { return 0x19; } }
        public short Size { get { return 8; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            Variable = (EnvironmentalVariable) reader.ReadByte();
            Red = reader.ReadShort();
            Green = reader.ReadShort();
            Blue = reader.ReadShort();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteByte((byte) Variable);
            stream.WriteShort(Red);
            stream.WriteShort(Green);
            stream.WriteShort(Blue);
            
            return this;
        }
    }
}
