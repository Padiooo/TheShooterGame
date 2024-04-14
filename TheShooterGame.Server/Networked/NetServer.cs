using LiteNetLib;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MonoLibrary.Engine.Network;
using MonoLibrary.Engine.Network.Managers;
using MonoLibrary.Engine.Services;
using MonoLibrary.Engine.Services.Updates;
using TheShooterGame.Server.Services;
using TheShooterGame.Server.Settings;
using TheShooterGame.Shared.Messages;

namespace TheShooterGame.Server.Networked
{
    public class NetServer : NetworkManagerServer
    {
        private readonly MapService _mapService;
        private readonly GameSettings _gameSettings;

        private readonly GameDataMessage _message;

        public NetServer(MapService map, IOptions<GameSettings> gameSettings,
                         INetworkFactory factory,
                         IUpdateLoop updater,
                         IOptions<NetworkSettings> settings,
                         IGameStateHub stateHub,
                         ILogger<NetServer> logger)
            : base(factory, updater, settings, stateHub, logger)
        {
            _mapService = map;
            _gameSettings = gameSettings.Value;
            _message = new GameDataMessage()
            {
                Player = new GameDataMessage.PlayerData()
                {
                    Size = _gameSettings.Player.Size,
                    LocalColor = _gameSettings.Player.LocalPlayerColor,
                    EnnemyColor = _gameSettings.Player.EnnemyPlayerColor
                },
                Bullet = new GameDataMessage.BulletData()
                {
                    Radius = _gameSettings.Bullet.Radius,
                    LocalColor = _gameSettings.Bullet.LocalBulletColor,
                    EnnemyColor = _gameSettings.Bullet.EnnemyBulletColor,
                },
                Wall = new GameDataMessage.WallData()
                {
                    Size = _gameSettings.Wall.Size,
                    Color = _gameSettings.Wall.WallColor
                }
            };

            PacketProcessor.RegisterSharedTypes();
        }

        public override void Start()
        {
            base.Start();

            _mapService.SpawnMap(this);
        }

        protected override NetPeer OnConnectionRequest(ConnectionRequest request)
        {
            var peer = base.OnConnectionRequest(request);
            if (peer is not null)
            {
                using var writer = WriterPool.Get();
                PacketProcessor.Write(writer.Item, _message);
                peer.Send(writer.Item, DeliveryMethod.ReliableOrdered);
            }

            return peer;
        }
    }
}
