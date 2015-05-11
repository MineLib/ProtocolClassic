using MineLib.Core;
using MineLib.Core.IO;
using ProtocolClassic.Enums;

namespace ProtocolClassic.Packets.Extension.Server
{
    public struct EnvSetWeatherTypePacket : IPacketWithSize
    {
        public WeatherType WeatherType;

        public byte ID { get { return 0x1F; } }
        public short Size { get { return 2; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            WeatherType = (WeatherType) reader.ReadByte();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteByte((byte) WeatherType);
            
            return this;
        }
    }
}
