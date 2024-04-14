using MonoLibrary.Engine.Network.Components;
using MonoLibrary.Engine.Network.Datas;
using MonoLibrary.Engine.Objects;

using System;

namespace TheShooterGame.Shared.Components
{
    public class NetworkDataComponent<T> : NetworkComponent
        where T : struct, IEquatable<T>
    {
        public NetVar<T> Data { get; } = new NetVar<T>();

        public NetworkDataComponent(GameObject owner) : base(owner)
        {
            AddNetVar(Data);
        }
    }
}
