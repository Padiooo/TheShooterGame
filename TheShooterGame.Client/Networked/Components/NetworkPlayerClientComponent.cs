using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

using MonoLibrary.Engine.Components.Renderers;
using MonoLibrary.Engine.Objects;
using MonoLibrary.Engine.Services.Inputs.KeyboardInputs;

using TheShooterGame.Client.Services;
using TheShooterGame.Shared.Components;

namespace TheShooterGame.Client.Networked.Components
{
    public class NetworkPlayerClientComponent : NetworkPlayerComponent
    {
        private IInput[] _inputs;

        public NetworkPlayerClientComponent(GameObject owner) : base(owner)
        {

        }

        protected override void ClientInit()
        {
            base.ClientInit();

            if (Identity.IsLocalPlayer)
            {
                var gameData = Owner.Game.Services.GetService<GameDataService>();
                Owner.Components.Get<UniversalTextureRendererComponent>().Color = gameData.PlayerData.LocalColor;
            }

            var gameInputs = Owner.Game.Services.GetService<GameInputs>();
            _inputs = new IInput[] { gameInputs.Up, gameInputs.Right, gameInputs.Down, gameInputs.Left };
        }

        protected override void UpdatePlayer(float time)
        {
            _velocity.Directions = new Vector2(GetAxis(_inputs[1].IsDown, _inputs[3].IsDown), GetAxis(_inputs[2].IsDown, _inputs[0].IsDown));
        }

        private static int GetAxis(bool positiv, bool negativ)
        {
            return (positiv ? 1 : 0) + (negativ ? -1 : 0);
        }
    }
}
