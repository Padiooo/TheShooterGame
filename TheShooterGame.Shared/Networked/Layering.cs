using MonoLibrary.Engine.Components.Colliders;

namespace TheShooterGame.Shared.Networked
{
    public class Layering
    {
        public const int Player = 0;
        public const int Projectile = 1;
        public const int PowerUp = 2;
        public const int Wall = 3;
        public const int Bound = 4;

        public static Layer Players => new("Players", new int[] { Player }, new int[] { Projectile, Wall, Bound });
        public static Layer Projectiles => new("Projectiles", new int[] { Projectile }, new int[] { Player, Wall });
        public static Layer PowerUps => new("Powerups", new int[] { PowerUp }, new int[] { Player, Wall });
        public static Layer Walls => new("Walls", new int[] { Wall }, new int[] { Player, Projectile, PowerUp, Bound });
        public static Layer Bounds => new("Bounds", new int[] { Bound }, new int[] { Player, PowerUp, Wall });
    }
}
