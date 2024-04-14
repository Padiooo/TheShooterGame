using Microsoft.Xna.Framework;

namespace TheShooterGame.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var game = new GameServer();

            while (!Console.KeyAvailable)
            {
                game.RunOneFrame();
            }
        }
    }
}
