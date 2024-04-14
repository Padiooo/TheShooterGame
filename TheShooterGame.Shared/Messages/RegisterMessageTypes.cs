using LiteNetLib.Utils;

using MonoLibrary.Engine.Network.Serializers;

namespace TheShooterGame.Shared.Messages
{
    public static class RegisterMessageTypes
    {
        public static void RegisterSharedTypes(this NetPacketProcessor packetProcessor)
        {
            packetProcessor.RegisterNestedType(NetSerialization.Write<GameDataMessage.PlayerData>, NetSerialization.Read<GameDataMessage.PlayerData>);
            packetProcessor.RegisterNestedType(NetSerialization.Write<GameDataMessage.BulletData>, NetSerialization.Read<GameDataMessage.BulletData>);
            packetProcessor.RegisterNestedType(NetSerialization.Write<GameDataMessage.WallData>, NetSerialization.Read<GameDataMessage.WallData>);
        }
    }
}
