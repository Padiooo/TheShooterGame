using LiteNetLib;
using LiteNetLib.Utils;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MonoLibrary.Engine.Network;
using MonoLibrary.Engine.Network.Managers;
using MonoLibrary.Engine.Network.Messages;
using MonoLibrary.Engine.Services;
using MonoLibrary.Engine.Services.Updates;

using TheShooterGame.Client.Services;
using TheShooterGame.Shared.Messages;
using TheShooterGame.Shared.Networked;

namespace TheShooterGame.Client.Networked
{
    public class NetClient : NetworkManagerClient
    {
        private GameDataService _dataService;

        public NetClient(GameDataService dataService,
                         INetworkFactory factory,
                         IUpdateLoop updater,
                         IOptions<NetworkSettings> settings,
                         IGameStateHub stateHub,
                         ILogger<NetworkManagerClient> logger)
            : base(factory, updater, settings, stateHub, logger)
        {
            PacketProcessor.RegisterSharedTypes();

            PacketProcessor.SubscribeReusable<GameDataMessage>(HandleGameData);

            _dataService = dataService;
        }

        protected override void OnConnect()
        {
            var writer = new NetDataWriter();
            PacketProcessor.Write(writer, new SpawnRequest() { NetworkOwnerId = Identity.NetId, PrefabId = (int)SpawnType.Player });
            Server.Send(writer, DeliveryMethod.ReliableOrdered);
        }

        private void HandleGameData(GameDataMessage message)
        {
            _dataService.PlayerData = message.Player;
            _dataService.BulletData = message.Bullet;
            _dataService.WallData = message.Wall;
        }
    }
}
