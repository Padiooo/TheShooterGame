using LiteNetLib.Utils;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;

using MonoLibrary.Engine;
using MonoLibrary.Engine.Components.Renderers;
using MonoLibrary.Engine.Network.Managers;
using MonoLibrary.Engine.Objects;

using TheShooterGame.Client.Networked.Components;
using TheShooterGame.Client.Services;
using TheShooterGame.Shared.Components;
using TheShooterGame.Shared.Networked;

namespace TheShooterGame.Client.Networked
{
    public class ClientFactory : NetFactory
    {
        private readonly GameDataService _dataService;

        public ClientFactory(GameEngine game, GameDataService dataService, IOptions<NetworkSettings> settings, ILogger<ClientFactory> logger)
            : base(game, settings, logger)
        {
            _dataService = dataService;
        }

        protected override GameObject SpawnPlayer(NetDataReader reader)
        {
            var go = base.SpawnPlayer(reader);
            var renderer = new UniversalTextureRendererComponent(go)
            {
                Texture = Game.Services.GetService<TextureService>().Square,
                Color = _dataService.PlayerData.EnnemyColor,
                Size = new(_dataService.PlayerData.Size)
            };
            go.Components.Add(renderer);

            return go;
        }

        protected override NetworkPlayerComponent BuildPlayerComponent(GameObject go) => new NetworkPlayerClientComponent(go);
        protected override NetworkShooterComponent BuildShooterComponent(GameObject go) => new NetworkShootClientComponent(go);

        protected override GameObject SpawnBullet(NetDataReader reader)
        {
            var go = base.SpawnBullet(reader);
            go.Components.Add(new UniversalTextureRendererComponent(go)
            {
                Texture = go.Game.Services.GetService<TextureService>().Circle,
                Color = _dataService.BulletData.EnnemyColor,
                Size = new Vector2(_dataService.BulletData.Radius)
            });
            go.Components.Add(new NetworkBulletClientComponent(go));

            return go;
        }

        protected override GameObject SpawnWall(NetDataReader reader)
        {
            var go = base.SpawnWall(reader);

            var renderer = new UniversalTextureRendererComponent(go)
            {
                Texture = go.Game.Services.GetService<TextureService>().Square,
                Size = new(_dataService.WallData.Size),
                Color = _dataService.WallData.Color
            };

            go.Components.Add(renderer);

            return go;
        }
    }
}
