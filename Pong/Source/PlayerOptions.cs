using System.Numerics;
using Guap.Input;

namespace Pong;

readonly struct PlayerOptions
{
    public readonly Vector2 StartingPosition;
    public readonly Key Up;
    public readonly Key Down;

    public PlayerOptions(Vector2 startingPosition, Key up, Key down)
    {
        StartingPosition = startingPosition;
        Up = up;
        Down = down;
    }
}