using Microsoft.Xna.Framework;

using MonoLibrary.Engine.Network.Components;
using MonoLibrary.Engine.Network.Datas;
using MonoLibrary.Engine.Objects;

namespace TheShooterGame.Shared.Components
{
    public abstract class NetworkShooterComponent : NetworkComponent
    {
        protected readonly NetVar<bool> _canShoot = new();

        protected IInvokable<Vector2> Shooter { get; private set; }

        public NetworkShooterComponent(GameObject owner) : base(owner)
        {
            AddNetVar(_canShoot);
        }

        protected sealed override void Init()
        {
            Shooter = Identity.RegisterCommand<Vector2>(Shoot);
        }

        protected virtual void Shoot(Vector2 direction) { }
    }
}
