using Microsoft.Xna.Framework;

namespace TheShooterGame.Shared.Networked.Datas
{
    public struct BulletSpawnData
    {
        public int NetOwnerId;
        public Vector2 SpawnPosition;
        public Vector2 Direction;
    }
}
