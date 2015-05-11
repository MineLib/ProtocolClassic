﻿using MineLib.Core;
using MineLib.Core.Data;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Server
{
    public struct PositionUpdatePacket : IPacketWithSize
    {
        public sbyte PlayerID;
        public Position ChangeLocation;

        public byte ID { get { return 0x0A; } }
        public short Size { get { return 5; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            PlayerID = reader.ReadSByte();
            ChangeLocation = Position.FromReaderSByte(reader);

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
            
            return this;
        }
    }
}
