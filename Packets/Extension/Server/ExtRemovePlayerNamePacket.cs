﻿using MineLib.Core;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Extension.Server
{
    public struct ExtRemovePlayerNamePacket : IPacketWithSize
    {
        public short NameID;

        public byte ID { get { return 0x18; } }
        public short Size { get { return 3; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            NameID = reader.ReadShort();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteShort(NameID);
            
            return this;
        }
    }
}
