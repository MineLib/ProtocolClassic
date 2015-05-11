﻿using System;

using MineLib.Core;
using MineLib.Core.Data;

using ProtocolClassic.Data;
using ProtocolClassic.Enums;
using ProtocolClassic.Packets.Server;

namespace ProtocolClassic
{
    public partial class Protocol
    {
        private void OnPacketHandled(int id, IPacketWithSize packet, ConnectionState? state)
        {
            if (!Connected)
                return;

            switch ((PacketsServer) id)
            {
                case PacketsServer.ServerIdentification:
                    State = ConnectionState.Joined;
                    break;

                case PacketsServer.Ping:
                    break;

                case PacketsServer.LevelInitialize:
                    break;

                case PacketsServer.LevelDataChunk:
                    var levelDataChunkPacket = (LevelDataChunkPacket) packet;
                    //Level.ReadFromSteam(levelDataChunkPacket.ChunkData);
                    break;

                case PacketsServer.LevelFinalize:
                    var levelFinalizePacket = (LevelFinalizePacket) packet;

                    var chunkList = Level.ReadFromArray(levelFinalizePacket.Coordinates);
                    OnChunkList(chunkList);
                    break;

                case PacketsServer.SetBlock:
                    var setBlockPacket = (SetBlockPacket) packet;

                    OnBlockChange(setBlockPacket.Coordinates, setBlockPacket.BlockType);
                    break;

                case PacketsServer.SpawnPlayer:
                    var spawnPlayerPacket = (SpawnPlayerPacket) packet;

                    OnSpawnPoint(spawnPlayerPacket.Coordinates);
                    break;

                case PacketsServer.PositionAndOrientationTeleport:
                    break;

                case PacketsServer.PositionAndOrientationUpdate:
                    var positionAndOrientationUpdatePacket = (PositionAndOrientationUpdatePacket) packet;

                    OnPlayerLook(new Vector3(positionAndOrientationUpdatePacket.Yaw, positionAndOrientationUpdatePacket.Pitch));
                    OnPlayerPosition(positionAndOrientationUpdatePacket.ChangeLocation);

                    break;

                case PacketsServer.PositionUpdate:
                    var positionUpdatePacket = (PositionUpdatePacket)packet;

                    OnPlayerPosition(positionUpdatePacket.ChangeLocation);
                    break;

                case PacketsServer.OrientationUpdate:
                    var orientationUpdatePacket = (OrientationUpdatePacket)packet;

                    OnPlayerLook(new Vector3(orientationUpdatePacket.Yaw, orientationUpdatePacket.Pitch));
                    break;

                case PacketsServer.DespawnPlayer:
                    break;

                case PacketsServer.Message:
                    var messagePacket = (MessagePacket) packet;

                    OnChatMessage(messagePacket.Message);
                    break;

                case PacketsServer.DisconnectPlayer:
                    break;

                case PacketsServer.UpdateUserType:
                    break;


                case PacketsServer.ExtInfo:
                    break;

                case PacketsServer.ExtEntry:
                    break;

                case PacketsServer.SetClickDistance:
                    break;

                case PacketsServer.CustomBlockSupportLevel:
                    break;

                case PacketsServer.HoldThis:
                    break;

                case PacketsServer.SetTextHotKey:
                    break;

                case PacketsServer.ExtAddPlayerName:
                    break;

                case PacketsServer.ExtRemovePlayerName:
                    break;

                case PacketsServer.EnvSetColor:
                    break;

                case PacketsServer.MakeSelection:
                    break;

                case PacketsServer.RemoveSelection:
                    break;

                case PacketsServer.SetBlockPermission:
                    break;

                case PacketsServer.ChangeModel:
                    break;

                case PacketsServer.EnvSetMapAppearance:
                    break;

                case PacketsServer.EnvSetWeatherType:
                    break;

                case PacketsServer.HackControl:
                    break;

                case PacketsServer.ExtAddEntity2:
                    break;

                default:
                    throw new ProtocolException("Connection error: Incorrect data.");
            }
        }
    }
}