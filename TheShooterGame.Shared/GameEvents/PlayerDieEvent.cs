using MonoLibrary.Engine.Network.Components;

using System.Runtime.CompilerServices;

namespace TheShooterGame.Shared.GameEvents
{
    [SkipLocalsInit]
    public readonly struct PlayerDieEvent
    {
        public readonly NetworkIdentityComponent Player;
        public readonly NetworkIdentityComponent Killer;

        public readonly float BaseDamage;
        public readonly float DamageDone;

        public PlayerDieEvent(NetworkIdentityComponent player, NetworkIdentityComponent killer, float baseDamage, float damageDone)
        {
            Player = player;
            Killer = killer;
            BaseDamage = baseDamage;
            DamageDone = damageDone;
        }
    }
}
