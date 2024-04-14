using MonoLibrary.Engine.Components.Colliders;
using MonoLibrary.Engine.Network.Components;
using MonoLibrary.Engine.Objects;

using TheShooterGame.Shared.Components;

namespace TheShooterGame.Server.Networked.Components
{
    public class NetworkBulletServerComponent : NetworkBulletComponent
    {
        private readonly Chrono chrono;

        public NetworkBulletServerComponent(GameObject owner, float damage, int ownerId, ColliderComponent collider) : base(owner)
        {
            OwnerId.Value = ownerId;
            chrono = new Chrono(0, 1f);
            chrono.OnTick += Chrono_OnTick;

            collider.OnCollisionEnter += (me, other) =>
            {
                if (other.Owner.Components.TryGet<NetworkIdentityComponent>(out var otherIdentity))
                {
                    if (otherIdentity.NetOwnerId != ownerId)
                    {
                        if (other.Owner.Components.TryGet<NetworkHealthComponent>(out var player))
                            player.TakeDamage(me.Owner, damage);

                        me.Owner.Destroy();
                    }
                }
            };
        }

        private void Chrono_OnTick(Chrono arg1, object arg2)
        {
            Owner.Destroy();
            chrono.OnTick -= Chrono_OnTick;
        }

        protected override void UpdateServer(float time)
        {
            chrono?.Update(time);
        }
    }
}
