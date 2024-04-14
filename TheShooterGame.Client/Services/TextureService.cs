using Microsoft.Xna.Framework.Graphics;

using MonoLibrary.Engine;
using MonoLibrary.Helpers;

namespace TheShooterGame.Client.Services
{
    public class TextureService
    {
        private readonly GameEngine Game;

        public Texture2D Square { get; }
        public Texture2D Circle { get; }

        public TextureService(GameEngine game)
        {
            Game = game;

            Square = game.GraphicsDevice.CreateSquare(50);
            Circle = game.GraphicsDevice.CreateCircleFill(256);
        }
    }
}
