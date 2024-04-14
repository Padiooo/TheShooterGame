using Microsoft.Extensions.DependencyInjection;

using MonoLibrary.Engine.Network.Components;
using MonoLibrary.Engine.Network.Datas;
using MonoLibrary.Engine.Objects;

using PubSub;

using System;
using TheShooterGame.Shared.GameEvents;

namespace TheShooterGame.Shared.Components
{
    public class NetworkHealthComponent : NetworkComponent
    {
        private readonly Hub _hub;

        public readonly NetVar<float> MaxHealth = new();
        public readonly NetVar<float> Health = new();

        public NetworkHealthComponent(GameObject owner) : base(owner)
        {
            _hub = owner.Game.Services.GetService<Hub>();
        }

        protected override void ServerInit()
        {
            MaxHealth.Value = 100;
            Health.Value = 100;
        }

        public float TakeDamage(GameObject from, float damage)
        {
            if (damage < 0)
                return 0;

            float health = Health.Value;
            Health.Value = MathF.Max(0, Health.Value - damage);

            float dealt = health - Health.Value;

            if (Health.Value == 0)
                _hub.Publish(new PlayerDieEvent(Identity, from.Components.Get<NetworkIdentityComponent>(), damage, dealt));

            return dealt;
        }
    }
}
