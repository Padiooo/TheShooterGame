using MonoLibrary.Engine.Network.Managers;
using MonoLibrary.Engine.Objects;
using MonoLibrary.Engine.Services.Updates;
using MonoLibrary.EventBus;

using PubSub;
using TheShooterGame.Shared.GameEvents;
using TheShooterGame.Shared.Networked;

namespace TheShooterGame.Server.Services
{
    internal class GamePlayService : IUpdaterService
    {
        private readonly List<IDisposable> _disposables = new();
        private readonly List<Chrono> _chronos = new();
        private readonly INetworkManager _networkManager;
        private readonly MapService _mapService;

        private readonly Random rng = new Random();

        public GamePlayService(INetworkManager networkManager, Hub hub, MapService mapService, IUpdateLoop updateLoop)
        {
            _networkManager = networkManager;
            _mapService = mapService;
            _disposables.Add(updateLoop.Register(this));
            _disposables.Add(hub.Register<PlayerDieEvent>(this, OnPlayerDied));
        }

        public void OnPlayerDied(PlayerDieEvent arg)
        {
            arg.Player.Owner.Destroy();
            var chrono = new Chrono(0, 3, false, arg.Player.NetOwnerId);
            chrono.OnTick += PlayerRespawn;
            _chronos.Add(chrono);
        }

        private void PlayerRespawn(Chrono chrono, object state)
        {
            int networkIdentity = (int)state;
            var netIdentity = _networkManager.GetIdentity(networkIdentity);
            _networkManager.Spawn(SpawnType.Player, _mapService.Spawns[rng.Next(_mapService.Spawns.Count)], netIdentity);
            _chronos.Remove(chrono);
            chrono.OnTick -= PlayerRespawn;
        }

        public void Update(float deltaTime)
        {
            for (int i = _chronos.Count - 1; i >= 0; i--)
                _chronos[i].Update(deltaTime);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}
