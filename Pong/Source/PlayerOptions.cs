using System.Numerics;
using Guap.Input;

namespace Pong;

readonly struct PlayerOptions
{
    public readonly Vector2 Start;
    public readonly float Speed;
    public readonly Func<Keyboard, Ball, Player, int> MoveDirection;
    public readonly Ball Ball;

    public PlayerOptions(float start, float speed, Func<Keyboard, Ball, Player, int> checkMove, Ball ball = null)
    {
        Start = new(start, 0f);
        Speed = speed;
        MoveDirection = checkMove;
        Ball = ball;
    }

    public PlayerOptions And(Ball ball) => new(Start.X, Speed, MoveDirection, ball);
}