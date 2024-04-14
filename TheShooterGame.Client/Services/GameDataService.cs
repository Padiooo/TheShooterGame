using TheShooterGame.Shared.Messages;

namespace TheShooterGame.Client.Services
{
    public class GameDataService
    {
        public GameDataMessage.PlayerData PlayerData { get; set; }
        public GameDataMessage.BulletData BulletData { get; set; }
        public GameDataMessage.WallData WallData { get; set; }
    }
}
