﻿using MineLib.Core;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Extension.Server
{
    public struct SetClickDistancePacket : IPacketWithSize
    {
        public short Distance;

        public byte ID { get { return 0x12; } }
        public short Size { get { return 3; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            Distance = reader.ReadShort();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteShort(Distance);
            
            return this;
        }
    }
}
