using LiteNetLib.Utils;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MonoLibrary.Engine;
using MonoLibrary.Engine.Network;
using MonoLibrary.Engine.Network.Components;
using MonoLibrary.Engine.Network.Managers;
using MonoLibrary.Engine.Objects;

using TheShooterGame.Shared.Components;

namespace TheShooterGame.Shared.Networked
{
    public enum SpawnType
    {
        Player,
        Bullet,
        Wall
    }

    public abstract class NetFactory : NetworkPrefabFactoryBase
    {
        protected readonly GameEngine Game;

        public NetFactory(GameEngine game, IOptions<NetworkSettings> settings, ILogger<NetFactory> logger) : base(settings, logger)
        {
            Game = game;

            RegisterPrefab(SpawnType.Player, SpawnPlayer);
            RegisterPrefab(SpawnType.Bullet, SpawnBullet);
            RegisterPrefab(SpawnType.Wall, SpawnWall);
        }

        protected virtual GameObject SpawnPlayer(NetDataReader reader)
        {
            var go = new GameObject(Game);
            // Shared
            {
                go.Components.Add(new NetworkVelocityComponent(go));
                go.Components.Add(new NetworkTransformComponent(go));
                go.Components.Add(BuildPlayerComponent(go));
                go.Components.Add(BuildShooterComponent(go));
                go.Components.Add(new NetworkHealthComponent(go));

                // NetworkShooterComponent
                // NetworkHealthComponent
            }
            // Server
            {
                // AABBColliderComponent
            }
            // Client
            {
                // UniversalTextureRendererComponent
                // HealthRendererComponent
            }

            return go;
        }

        protected virtual NetworkPlayerComponent BuildPlayerComponent(GameObject go) => new NetworkPlayerComponent(go);
        protected abstract NetworkShooterComponent BuildShooterComponent(GameObject go);

        protected virtual GameObject SpawnBullet(NetDataReader reader)
        {
            var go = new GameObject(Game);

            // Shared
            {
                go.Components.Add(new NetworkVelocityComponent(go));
                go.Components.Add(new NetworkTransformComponent(go));
            }
            // Server
            {
                // NetworkBulletComponent
                // CircleColliderComponent
            }
            // Client
            {
                // UniversalTextureRendererComponent
            }

            return go;
        }

        protected virtual GameObject SpawnWall(NetDataReader reader)
        {
            var go = new GameObject(Game);

            // Shared
            {
                go.Components.Add(new NetworkTransformComponent(go));
            }
            // Server
            {
                // AABBColliderComponent
            }
            // Client
            {
                // UniversalTextureRendererComponent
            }

            return go;
        }
    }
}
