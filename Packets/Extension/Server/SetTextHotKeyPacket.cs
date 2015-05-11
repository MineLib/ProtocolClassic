﻿using MineLib.Core;
using MineLib.Core.IO;
using ProtocolClassic.Enums;

namespace ProtocolClassic.Packets.Extension.Server
{
    public struct SetTextHotKeyPacket : IPacketWithSize
    {
        public string Label;
        public string Action;
        public int KeyCode;
        public KeyMods KeyMods;

        public byte ID { get { return 0x15; } }
        public short Size { get { return 134; } }

        public IPacketWithSize ReadPacket(IProtocolDataReader reader)
        {
            Label = reader.ReadString();
            Action = reader.ReadString();
            KeyCode = reader.ReadInt();
            KeyMods = (KeyMods) reader.ReadByte();

            return this;
        }

        IPacket IPacket.ReadPacket(IProtocolDataReader reader)
        {
            return ReadPacket(reader);
        }

        public IPacket WritePacket(IProtocolStream stream)
        {
            stream.WriteString(Label);
            stream.WriteString(Action);
            stream.WriteInt(KeyCode);
            stream.WriteByte((byte) KeyMods);
            
            return this;
        }
    }
}
