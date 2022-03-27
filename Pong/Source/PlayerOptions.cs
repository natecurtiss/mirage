using System;
using System.Numerics;
using Guap.Input;
using Timer = Guap.Timer;

namespace Pong;

readonly struct PlayerOptions
{
    public readonly PlayerIndex Index;
    public readonly Vector2 StartingPosition;
    public readonly float Speed;
    public readonly float ServeDelay;
    public readonly Func<Keyboard, Ball, Player, int> MoveDirection;
    public readonly Func<Keyboard, Timer, bool> ShouldServe;
    public readonly Ball Ball;

    public PlayerOptions(PlayerIndex index, float start, float speed, float serveDelay, Func<Keyboard, Ball, Player, int> checkMove,  Func<Keyboard, Timer, bool> checkServe, Ball ball = null)
    {
        Index = index;
        StartingPosition = new(start, 0f);
        Speed = speed;
        ServeDelay = serveDelay;
        MoveDirection = checkMove;
        ShouldServe = checkServe;
        Ball = ball;
    }

    public PlayerOptions And(Ball ball) => new(Index, StartingPosition.X, Speed, ServeDelay, MoveDirection, ShouldServe, ball);
}