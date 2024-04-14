using Microsoft.Xna.Framework.Input;

using MonoLibrary.Engine.Services.Inputs.KeyboardInputs;

namespace TheShooterGame.Client.Services
{
    public class GameInputs
    {
        public readonly IInput Up, Right, Down, Left;

        public GameInputs(IKeyboardInputService service)
        {
            Up = service.CreateInput(Keys.Z);
            Right = service.CreateInput(Keys.D);
            Down = service.CreateInput(Keys.S);
            Left = service.CreateInput(Keys.Q);
        }
    }
}
