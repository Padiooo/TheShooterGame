using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoLibrary.Engine.Components.Interfaces;
using MonoLibrary.Engine.Objects;
using MonoLibrary.Helpers;

using TheShooterGame.Shared.Components;

namespace TheShooterGame.Client.Networked.Components
{
    public class HealthRendererComponent : IDrawComponent
    {
        private const float Width = 70;
        private const float Height = 10;

        private static readonly Point Size = new((int)Width, (int)Height);

        private readonly NetworkHealthComponent healthComponent;

        public GameObject Owner { get; }

        public HealthRendererComponent(GameObject owner, NetworkHealthComponent healthComponent)
        {
            Owner = owner;
            this.healthComponent = healthComponent;
        }

        public void Draw(float time, SpriteBatch spriteBatch)
        {
            var size = new Point((int)(Width * (healthComponent.Health.Value / healthComponent.MaxHealth.Value)), (int)Height);
            spriteBatch.DrawRectFill(new Rectangle(Owner.Position.ToPoint() + new Point((int)-Width / 2, -50), Size), color: Color.Black, layerDepth: 0.5f);
            spriteBatch.DrawRectFill(new Rectangle(Owner.Position.ToPoint() + new Point((int)-Width / 2, -50), size), Color.Green, layerDepth: 0.4f);
            spriteBatch.DrawRectStroke(new Rectangle(Owner.Position.ToPoint() + new Point((int)-Width / 2, -50), Size), color: Color.Black, layerDepth: 0.3f);
        }
    }
}
