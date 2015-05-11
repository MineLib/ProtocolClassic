﻿using MineLib.Core;
using MineLib.Core.Data;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Server
{
    public struct PositionAndOrientationUpdatePacket : IPacketWithSize
    {
        public sbyte PlayerID;
        public Position ChangeLocation;
        public byte Yaw;
        public byte Pitch;

        public byte ID { get { return 0x09; } }
        public short Size { get { return 7; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            PlayerID = reader.ReadSByte();
            ChangeLocation = Position.FromReaderSByte(reader);
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
            ChangeLocation.ToStreamSByte(stream);
            stream.WriteByte(Yaw);
            stream.WriteByte(Pitch);
            
            return this;
        }
    }
}
