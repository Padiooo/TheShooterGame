using Microsoft.Xna.Framework;

using MonoLibrary.Dependency;
using MonoLibrary.Helpers;

namespace TheShooterGame.Server.Settings
{
    [Settings(nameof(GameSettings))]
    public class GameSettings
    {
        public string MapFile { get; set; }
        public PlayerSettings Player { get; set; } = new();
        public BulletSetting Bullet { get; set; } = new();
        public WallSettings Wall { get; set; } = new();
    }

    public class PlayerSettings
    {
        public float Size { get; set; } = 30f;
        public float Speed { get; set; } = 200f;
        public float ShootDelay { get; set; } = 0.7f;

        public string LocalColor { get; set; } = Color.Blue.ToHex();
        public Color LocalPlayerColor => LocalColor.StartsWith('#') ? LocalColor.FromHex() : LocalColor.ToColor();

        public string EnnemyColor { get; set; } = Color.Red.ToHex();
        public Color EnnemyPlayerColor => EnnemyColor.StartsWith('#') ? EnnemyColor.FromHex() : EnnemyColor.ToColor();
    }

    public class BulletSetting
    {
        public float Radius { get; set; } = 12f;
        public float Speed { get; set; } = 350f;

        public float Damage { get; set; } = 35f;

        public string LocalColor { get; set; } = Color.Blue.ToHex();
        public Color LocalBulletColor => LocalColor.StartsWith('#') ? LocalColor.FromHex() : LocalColor.ToColor();

        public string EnnemyColor { get; set; } = Color.Red.ToHex();
        public Color EnnemyBulletColor => EnnemyColor.StartsWith('#') ? EnnemyColor.FromHex() : EnnemyColor.ToColor();
    }

    public class WallSettings
    {
        public float Size { get; set; } = 50f;

        public string Color { get; set; } = Microsoft.Xna.Framework.Color.MonoGameOrange.ToHex();
        public Color WallColor => Color.StartsWith('#') ? Color.FromHex() : Color.ToColor();
    }
}
