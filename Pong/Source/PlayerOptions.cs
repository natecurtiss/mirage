using System.Numerics;
using Guap.Input;
using Timer = Guap.Timer;

namespace Pong;

readonly struct PlayerOptions
{
    public readonly PlayerNumber Number;
    public readonly Vector2 Start;
    public readonly float Speed;
    public readonly float ServeDelay;
    public readonly Func<Keyboard, Ball, Player, int> MoveDirection;
    public readonly Func<Keyboard, Timer, bool> ShouldServe;
    public readonly Ball Ball;

    public PlayerOptions(PlayerNumber number, float start, float speed, float serveDelay, Func<Keyboard, Ball, Player, int> checkMove,  Func<Keyboard, Timer, bool> checkServe, Ball ball = null)
    {
        Number = number;
        Start = new(start, 0f);
        Speed = speed;
        ServeDelay = serveDelay;
        MoveDirection = checkMove;
        ShouldServe = checkServe;
        Ball = ball;
    }

    public PlayerOptions And(Ball ball) => new(Number, Start.X, Speed, ServeDelay, MoveDirection, ShouldServe, ball);
}