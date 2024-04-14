using LiteNetLib.Utils;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MonoLibrary.Engine;
using MonoLibrary.Engine.Components.Colliders;
using MonoLibrary.Engine.Network.Managers;
using MonoLibrary.Engine.Network.Serializers;
using MonoLibrary.Engine.Objects;

using System.Numerics;

using TheShooterGame.Server.Networked.Components;
using TheShooterGame.Server.Services;
using TheShooterGame.Server.Settings;
using TheShooterGame.Shared.Components;
using TheShooterGame.Shared.Networked;
using TheShooterGame.Shared.Networked.Datas;

namespace TheShooterGame.Server.Networked
{
    public class ServerFactory : NetFactory
    {
        private readonly MapService _mapService;
        private readonly GameSettings _gameSettings;
        private int index = 0;

        public ServerFactory(GameEngine game, MapService mapService, IOptions<GameSettings> gameSettings, IOptions<NetworkSettings> settings, ILogger<NetFactory> logger)
            : base(game, settings, logger)
        {
            _mapService = mapService;
            _gameSettings = gameSettings.Value;
        }

        protected override GameObject SpawnPlayer(NetDataReader reader)
        {
            var go = base.SpawnPlayer(reader);
            go.Position = _mapService.Spawns[index++ % _mapService.Spawns.Count];

            var velocity = go.Components.Get<NetworkVelocityComponent>();
            velocity.Speed = _gameSettings.Player.Speed;

            var collider = new AABBColliderComponent(go)
            {
                Size = new(_gameSettings.Player.Size),
                Layer = Layering.Players,
            };
            go.Components.Add<ColliderComponent>(collider);

            return go;
        }

        protected override NetworkShooterComponent BuildShooterComponent(GameObject go) => new NetworkShootServerComponent(go);

        protected override GameObject SpawnBullet(NetDataReader reader)
        {
            var go = base.SpawnBullet(reader);

            var data = reader.Read<BulletSpawnData>();

            go.Position = data.SpawnPosition;
            var velocity = go.Components.Get<NetworkVelocityComponent>();
            velocity.Directions = data.Direction;
            velocity.Speed = _gameSettings.Bullet.Speed;

            var collider = new CircleColliderComponent(go)
            {
                Radius = _gameSettings.Bullet.Radius,
                Layer = Layering.Projectiles,
            };

            go.Components.Add<ColliderComponent>(collider);
            go.Components.Add(new NetworkBulletServerComponent(go, _gameSettings.Bullet.Damage, data.NetOwnerId, collider));

            return go;
        }

        protected override GameObject SpawnWall(NetDataReader reader)
        {
            var go = base.SpawnWall(reader);

            var position = reader.Read<Vector2>();
            go.Position = position;

            var collider = new AABBColliderComponent(go)
            {
                Layer = Layering.Walls,
                Size = new(_gameSettings.Wall.Size),
            };
            go.Components.Add<ColliderComponent>(collider);

            collider.OnColliding += (me, other) =>
            {
                if (other.Owner.Components.TryGet<NetworkVelocityComponent>(out var comp))
                {
                    var delta = comp.LastPosition - comp.Owner.Position;
                    delta /= 10;
                    int tries = 0;
                    while (me.IsColliding(other) && tries++ < 50)
                        other.Owner.Position += delta;
                }
            };


            return go;
        }
    }
}
