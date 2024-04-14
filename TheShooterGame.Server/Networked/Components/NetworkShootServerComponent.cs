using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;

using MonoLibrary.Engine.Network.Managers;
using MonoLibrary.Engine.Objects;

using TheShooterGame.Server.Settings;
using TheShooterGame.Shared.Components;
using TheShooterGame.Shared.Networked;
using TheShooterGame.Shared.Networked.Datas;

namespace TheShooterGame.Server.Networked.Components
{
    public class NetworkShootServerComponent : NetworkShooterComponent
    {
        private float _reloadTimer, _reloadInterval;

        public NetworkShootServerComponent(GameObject owner) : base(owner)
        {
        }

        protected override void ServerInit()
        {
            _reloadInterval = Owner.Game.Services.GetService<IOptions<GameSettings>>().Value.Player.ShootDelay;
        }

        protected override void UpdateServer(float time)
        {
            if(!_canShoot.Value)
            {
                _reloadTimer -= time;
                if (_reloadTimer <= 0)
                    _canShoot.Value = true;
            }
        }

        protected override void Shoot(Vector2 direction)
        {
            // maybe client can send multiple commands before _canShoot get synced
            if (!_canShoot.Value)
                return;

            _canShoot.Value = false;
            _reloadTimer = _reloadInterval;

            Identity.Manager.Spawn<BulletSpawnData>(SpawnType.Bullet, new BulletSpawnData()
            {
                NetOwnerId = Identity.NetOwnerId,
                SpawnPosition = Owner.Position,
                Direction = direction
            });
        }
    }
}
