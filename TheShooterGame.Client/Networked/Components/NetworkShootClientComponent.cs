using Microsoft.Xna.Framework.Input;

using MonoLibrary.Engine.Objects;

using TheShooterGame.Shared.Components;

namespace TheShooterGame.Client.Networked.Components
{
    public class NetworkShootClientComponent : NetworkShooterComponent
    {
        public NetworkShootClientComponent(GameObject owner) : base(owner)
        {

        }

        protected override void UpdatePlayer(float time)
        {
            if (!Owner.Game.IsActive || !_canShoot.Value)
                return;

            var state = Mouse.GetState();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                Shooter.Invoke(state.Position.ToVector2() - Owner.Position);
        }
    }
}
