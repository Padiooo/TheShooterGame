using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;

using MonoLibrary.Engine.Network.Managers;

using TheShooterGame.Server.Settings;
using TheShooterGame.Shared.Networked;

namespace TheShooterGame.Server.Services
{
    public class MapService
    {
        private readonly GameSettings _gameSettings;
        public readonly List<Vector2> Spawns = new();

        public MapService(IOptions<GameSettings> options)
        {
            _gameSettings = options.Value;
        }

        public void SpawnMap(INetworkManager manager)
        {
            int y = 0;
            const int unitSize = 50;
            const int halfUnitSize = unitSize / 2;
            using (var reader = new StreamReader(File.OpenRead(_gameSettings.MapFile)))
            {
                int playerId = 0;
                string line = null;
                while ((line = reader.ReadLine()?.ToUpperInvariant()) != null)
                {
                    int x = 0;
                    foreach (var c in line)
                    {
                        var position = new Vector2(x * unitSize + halfUnitSize, y * unitSize + halfUnitSize);

                        switch (c)
                        {
                            case 'X':
                                manager.Spawn(SpawnType.Wall, position, null);
                                break;
                            case 'O':
                                Spawns.Add(position);
                                break;
                        }
                        x++;
                    }
                    y++;
                }
            }
        }
    }
}
