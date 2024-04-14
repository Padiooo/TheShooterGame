using MonoLibrary.Engine.Objects;

namespace TheShooterGame.Shared.GameEvents
{
    public readonly struct BulletHitEvent
    {
        public readonly GameObject Bullet;
        public readonly GameObject Object;

        public readonly float Damage;
    }
}
