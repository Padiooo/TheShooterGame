using MonoLibrary.Engine.Network.Components;
using MonoLibrary.Engine.Network.Datas;
using MonoLibrary.Engine.Objects;

namespace TheShooterGame.Shared.Components
{
    public class NetworkBulletComponent : NetworkComponent
    {
        public readonly NetVar<int> OwnerId;

        public NetworkBulletComponent(GameObject owner) : base(owner)
        {
            AddNetVar(OwnerId = new NetVar<int>());
        }
    }
}
