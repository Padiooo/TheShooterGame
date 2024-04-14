using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoLibrary.Dependency.Loggers.FileLogger;
using MonoLibrary.Engine;
using MonoLibrary.Engine.Network;
using MonoLibrary.Engine.Network.Managers;
using MonoLibrary.Engine.Services.Inputs.KeyboardInputs;

using PubSub;

using TheShooterGame.Client.Networked;
using TheShooterGame.Client.Services;

namespace TheShooterGame.Client
{
    public class GameClient : GameEngine
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private NetClient client;

        private SpriteFont _font;

        public GameClient()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.Title = "The Shooter";
            Window.AllowUserResizing = false;
            _ = new WindowService(this);

            client = (NetClient)Services.GetRequiredService<INetworkManager>();

            client.Start();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here

            _font = Content.Load<SpriteFont>("DefaultFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            base.Draw(gameTime);
            _spriteBatch.DrawString(_font, client.Identity?.NetId.ToString() ?? string.Empty, new Vector2(0), Color.Black);
            _spriteBatch.End();
        }

        protected override void OnConfigureLogging(ILoggingBuilder logBuilder)
        {
            base.OnConfigureLogging(logBuilder);

            logBuilder.AddFileLogger();
        }

        protected override void OnConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            base.OnConfigureServices(services, configuration);

            services.AddSingleton<GraphicsDeviceManager>(_graphics);
            services.AddSingleton<IGraphicsDeviceService, GraphicsDeviceManager>(provider => _graphics);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            services.AddSingleton<SpriteBatch>(_spriteBatch);

            services.AddSingleton<GameDataService>();

            services.AddLogging(builder => builder.AddFileLogger());

            services.AddScoped<Hub>();
            services.AddScoped<INetworkFactory, ClientFactory>();
            services.AddScoped<INetworkManager, NetClient>();

            services.AddScoped<IKeyboardInputService, KeyboardService>();
            services.AddScoped<GameInputs>();
            services.AddScoped<TextureService>();
        }
    }
}
