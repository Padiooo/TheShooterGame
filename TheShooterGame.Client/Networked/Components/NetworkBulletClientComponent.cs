using Microsoft.Extensions.DependencyInjection;

using MonoLibrary.Engine.Components.Renderers;
using MonoLibrary.Engine.Objects;

using TheShooterGame.Client.Services;
using TheShooterGame.Shared.Components;

namespace TheShooterGame.Client.Networked.Components
{
    public class NetworkBulletClientComponent : NetworkBulletComponent
    {
        public NetworkBulletClientComponent(GameObject owner) : base(owner)
        {
        }

        protected override void ClientInit()
        {
            if (OwnerId.Value == Identity.Manager.Identity.NetId)
            {
                var gameData = Owner.Game.Services.GetService<GameDataService>();
                Owner.Components.Get<UniversalTextureRendererComponent>().Color = gameData.BulletData.LocalColor;
            }
        }
    }
}
