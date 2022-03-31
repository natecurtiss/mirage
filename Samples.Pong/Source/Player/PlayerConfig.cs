using Mirage.Input;

namespace Samples.Pong;

readonly struct PlayerConfig
{
    public readonly PlayerIndex Index;
    public readonly Vector2 StartingPosition;
    public readonly float Speed;
    public readonly Func<Keyboard, Ball, Moveable, int> MoveDirection;
    public readonly Func<Keyboard, bool> ShouldServe;
    public readonly Ball Ball;

    public PlayerConfig(PlayerIndex index, float start, float speed, Func<Keyboard, Ball, Moveable, int> checkMove,  Func<Keyboard, bool> checkServe, Ball ball = null)
    {
        Index = index;
        StartingPosition = new(start, 0f);
        Speed = speed;
        MoveDirection = checkMove;
        ShouldServe = checkServe;
        Ball = ball;
    }

    public PlayerConfig And(Ball ball) => new(Index, StartingPosition.X, Speed, MoveDirection, ShouldServe, ball);
}