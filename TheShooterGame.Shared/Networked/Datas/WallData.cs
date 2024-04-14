using Microsoft.Xna.Framework;

using System;

namespace TheShooterGame.Shared.Networked.Datas
{
    public struct WallData : IEquatable<WallData>
    {
        public Vector2 Position;
        public Vector2 Size;

        public readonly bool Equals(WallData other)
        {
            return other.Position == Position
                && other.Size == Size;
        }
    }
}
