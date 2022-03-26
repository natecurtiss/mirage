using System.Numerics;

namespace Pong;

sealed class Player : Paddle
{
    protected override void OnUpdate(float dt)
    {
        if (Keyboard.IsDown(Options.Up))
        {
            if (Keyboard.IsUp(Options.Down))
                Position += new Vector2(0f, Options.Speed * dt);
        }
        else if (Keyboard.IsDown(Options.Down))
        {
            Position -= new Vector2(0f, Options.Speed * dt);
        }
    }
}