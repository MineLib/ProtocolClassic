using MineLib.Core;
using MineLib.Core.Data;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Extension.Server
{
    public struct ExtAddEntity2Packet : IPacketWithSize
    {
        public byte EntityID;
        public string InGameName;
        public string SkinName;
        public Position Spawn;
        public byte SpawnYaw;
        public byte SpawnPitch;

        public byte ID { get { return 0x21; } }
        public short Size { get { return 138; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            EntityID = reader.ReadByte();
            InGameName = reader.ReadString();
            SkinName = reader.ReadString();
            Spawn = Position.FromReaderShort(reader);
            SpawnYaw = reader.ReadByte();
            SpawnPitch = reader.ReadByte();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteByte(EntityID);
            stream.WriteString(InGameName);
            stream.WriteString(SkinName);
            Spawn.ToStreamShort(stream);
            stream.WriteByte(SpawnYaw);
            stream.WriteByte(SpawnPitch);
            
            return this;
        }
    }
}
