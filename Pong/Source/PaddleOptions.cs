using System.Numerics;
using Guap.Input;

namespace Pong;

readonly struct PaddleOptions
{
    public readonly Vector2 StartingPosition;
    public readonly float Speed;
    public readonly Key Up;
    public readonly Key Down;

    public PaddleOptions(Vector2 startingPosition, Key up, Key down, float speed)
    {
        StartingPosition = startingPosition;
        Up = up;
        Down = down;
        Speed = speed;
    }
}