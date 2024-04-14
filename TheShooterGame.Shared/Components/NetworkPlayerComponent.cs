using MonoLibrary.Engine.Network.Components;
using MonoLibrary.Engine.Objects;

namespace TheShooterGame.Shared.Components
{
    public class NetworkPlayerComponent : NetworkComponent
    {
        protected NetworkVelocityComponent _velocity;

        public NetworkPlayerComponent(GameObject owner) : base(owner)
        {
            _velocity = Owner.Components.Get<NetworkVelocityComponent>();
        }
    }
}
