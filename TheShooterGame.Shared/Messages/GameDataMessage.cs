using Microsoft.Xna.Framework;

namespace TheShooterGame.Shared.Messages
{
    public class GameDataMessage
    {
        public PlayerData Player { get; set; }
        public BulletData Bullet { get; set; }
        public WallData Wall { get; set; }

        public struct PlayerData
        {
            public float Size { get; set; }
            public Color LocalColor { get; set; }
            public Color EnnemyColor { get; set; }
        }

        public struct BulletData
        {
            public float Radius { get; set; }
            public Color LocalColor { get; set; }
            public Color EnnemyColor { get; set; }
        }

        public struct WallData
        {
            public float Size { get; set; }
            public Color Color { get; set; }
        }
    }
}
