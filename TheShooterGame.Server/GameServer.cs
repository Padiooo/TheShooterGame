using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;

using MonoLibrary.Dependency;
using MonoLibrary.Dependency.Loggers.ConsoleLogger;
using MonoLibrary.Dependency.Loggers.FileLogger;
using MonoLibrary.Engine;
using MonoLibrary.Engine.Network;
using MonoLibrary.Engine.Network.Managers;
using MonoLibrary.Engine.Services.Collision;
using MonoLibrary.Engine.Services.Collision.Algorithms;

using PubSub;

using TheShooterGame.Server.Networked;
using TheShooterGame.Server.Services;

namespace TheShooterGame.Server
{
    public class GameServer : GameEngine
    {
        public GameServer()
        {
            var v = new GraphicsDeviceManager(this);
            v.Dispose();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1 / 60f);
            //TargetElapsedTime = TimeSpan.FromSeconds(0.5);
        }

        protected override void Initialize()
        {
            base.Initialize();

            // TODO find a better way to add hooks
            _ = Services.GetService<GamePlayService>();

            var manager = Services.GetRequiredService<INetworkManager>();
            manager.Start();
        }

        protected override void Update(GameTime gameTime)
        {
            SuppressDraw();

            base.Update(gameTime);
        }

        protected override void OnConfigureLogging(ILoggingBuilder logBuilder)
        {
            logBuilder
                .AddColorConsoleLogger()
                .AddFileLogger();
        }

        protected override void OnConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            base.OnConfigureServices(services, configuration);


            services.AddSettings(typeof(GameServer).Assembly, configuration);

            services.AddScoped<Hub>();
            services.AddScoped<GamePlayService>();
            services.AddScoped<MapService>();
            services.AddScoped<INetworkManager, NetServer>();
            services.AddScoped<ICollisionService, CollisionService>();
            services.AddScoped<ICollisionAlgorithm, BruteForceCollisionAlgorithm>();
            services.AddScoped<INetworkFactory, ServerFactory>();
        }
    }
}
