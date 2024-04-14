using Microsoft.Xna.Framework;

using MonoLibrary.Engine.Network.Components;
using MonoLibrary.Engine.Network.Datas;
using MonoLibrary.Engine.Objects;

namespace TheShooterGame.Shared.Components
{
    public class NetworkVelocityComponent : NetworkComponent
    {
        private readonly NetVar<Vector2> _directions;
        private readonly NetVar<float> _speed;

        /// <summary>
        /// <see langword="value"/> always normalized.
        /// </summary>
        public Vector2 Directions
        {
            get => _directions.Value;
            set
            {
                if (value != Vector2.Zero)
                    value.Normalize();
                _directions.Value = value;
            }
        }

        public float Speed
        {
            get => _speed.Value;
            set => _speed.Value = value;
        }

        private Vector2 _lastPosition = Vector2.Zero;
        public Vector2 LastPosition => _lastPosition;

        public NetworkVelocityComponent(GameObject owner) : base(owner)
        {
            _directions = new NetVar<Vector2>();
            AddNetVar(_directions);
            _speed = new NetVar<float>();
            AddNetVar(_speed);
        }

        protected override void UpdateServer(float time)
        {
            _lastPosition = Owner.Position;
            Owner.Position += time * _speed.Value * _directions.Value;
        }
    }
}
