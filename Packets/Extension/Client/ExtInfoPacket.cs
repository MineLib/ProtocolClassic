﻿using MineLib.Core;
using MineLib.Core.IO;

namespace ProtocolClassic.Packets.Extension.Client
{
    public struct ExtInfoPacket : IPacketWithSize
    {
        public string AppName;
        public short ExtensionCount;

        public byte ID { get { return 0x10; } }
        public short Size { get { return 67; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            AppName = reader.ReadString();
            ExtensionCount = reader.ReadShort();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteString(AppName);
            stream.WriteShort(ExtensionCount);
            
            return this;
        }
    }
}
