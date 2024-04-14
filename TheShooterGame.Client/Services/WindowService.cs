using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using MonoLibrary.Engine;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace TheShooterGame.Client.Services
{
    public class WindowService
    {
        private const int Width = 1600, Height = 900;

        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        public WindowService(GameEngine game)
        {
            _graphicsDeviceManager = game.Services.GetService<GraphicsDeviceManager>();
            _graphicsDeviceManager.PreferredBackBufferWidth = Width;
            _graphicsDeviceManager.PreferredBackBufferHeight = Height;
            _graphicsDeviceManager.ApplyChanges();

            game.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            _graphicsDeviceManager.PreferredBackBufferWidth = Width;
            _graphicsDeviceManager.PreferredBackBufferHeight = Height;
            _graphicsDeviceManager.ApplyChanges();
        }
    }
}
