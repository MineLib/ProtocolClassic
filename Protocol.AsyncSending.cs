using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MineLib.Core;
using MineLib.Core.Data.Structs;
using ProtocolClassic.Enums;
using ProtocolClassic.Packets.Client;

namespace ProtocolClassic
{
    public partial class Protocol
    {
        private Dictionary<Type, Func<ISendingAsyncArgs, Task>> SendingAsyncHandlers { get; set; }

        public void RegisterSending(Type sendingAsyncType, Func<ISendingAsyncArgs, Task> func)
        {
            var any = sendingAsyncType.GetTypeInfo().ImplementedInterfaces.Any(p => p == typeof(ISendingAsync));
            if (!any)
                throw new InvalidOperationException("AsyncSending type must implement MineLib.Network.IAsyncSending");

            SendingAsyncHandlers[sendingAsyncType] = func;
        }

        private void RegisterSupportedSendings()
        {
            RegisterSending(typeof(ConnectToServerAsync), ConnectToServerAsync);
            RegisterSending(typeof(PlayerMovedAsync), PlayerMovedAsync);
            RegisterSending(typeof(PlayerSetRemoveBlockAsync), PlayerSetRemoveBlockAsync);
            RegisterSending(typeof(SendMessageAsync), SendMessageAsync);
        }

        public Task DoSendingAsync(Type sendingAsyncType, ISendingAsyncArgs args)
        {
            var any = sendingAsyncType.GetTypeInfo().ImplementedInterfaces.Any(p => p == typeof(ISendingAsync));
            if (!any)
                throw new InvalidOperationException("AsyncSending type must implement MineLib.Network.IAsyncSending");

            return SendingAsyncHandlers[sendingAsyncType](args);
        }

        public void DoSending(Type sendingType, ISendingAsyncArgs args)
        {
            var any = sendingType.GetTypeInfo().ImplementedInterfaces.Any(p => p == typeof(ISendingAsync));
            if (!any)
                throw new InvalidOperationException("AsyncSending type must implement MineLib.Network.IAsyncSending");

            SendingAsyncHandlers[sendingType](args).Wait();
        }


        private Task ConnectToServerAsync(ISendingAsyncArgs args)
        {
            var data = (ConnectToServerAsyncArgs) args;

            State = ConnectionState.Joining;

            return SendPacketAsync(new PlayerIdentificationPacket
            {
                ProtocolVersion = 0x07,
                Username        = _minecraft.ClientUsername,
                VerificationKey = _minecraft.AccessToken,
                UnUsed          = 0x42
            });
        }

        private Task PlayerMovedAsync(ISendingAsyncArgs args)
        {
            var data = (PlayerMovedAsyncArgs)args;
            switch (data.Mode)
            {
                case PlaverMovedMode.OnGround:
                {
                    var pdata = (PlaverMovedDataOnGround)data.Data;
                    return null;
                }

                case PlaverMovedMode.Vector3:
                {
                    var pdata = (PlaverMovedDataVector3)data.Data;
                    return null;
                }

                case PlaverMovedMode.YawPitch:
                {
                    var pdata = (PlaverMovedDataYawPitch)data.Data;
                    return null;
                }

                case PlaverMovedMode.All:
                {
                    var pdata = (PlaverMovedDataAll)data.Data;

                    return SendPacketAsync(new PositionAndOrientationPacket
                    {
                        Position =      pdata.Vector3,
                        Yaw = (byte)    pdata.Yaw,
                        Pitch = (byte)  pdata.Pitch,
                        PlayerID = 255
                    });
                }

                default:
                    throw new Exception("PacketError");
            }
        }

        private Task PlayerSetRemoveBlockAsync(ISendingAsyncArgs args)
        {
            var data = (PlayerSetRemoveBlockAsyncArgs)args;
            switch (data.Mode)
            {
                case PlayerSetRemoveBlockMode.Place:
                    {
                        var pdata = (PlayerSetRemoveBlockDataPlace) data.Data;
                        return null;
                    }

                case PlayerSetRemoveBlockMode.Dig:
                    {
                        var pdata = (PlayerSetRemoveBlockDataDig) data.Data;
                        return null;
                    }

                case PlayerSetRemoveBlockMode.Remove:
                    {
                        var pdata = (PlayerSetRemoveBlockDataRemove) data.Data;

                        return SendPacketAsync(new SetBlockPacket
                        {
                            Coordinates =           pdata.Location,
                            BlockType = (byte)      pdata.BlockID,
                            Mode = (SetBlockMode)   pdata.Mode
                        });
                    }

                default:
                    throw new Exception("PacketError");
            }
        }

        private Task SendMessageAsync(ISendingAsyncArgs args)
        {
            var data = (SendMessageAsyncArgs) args;

            return SendPacketAsync(new MessagePacket { Message = data.Message });
        }
    }
}
