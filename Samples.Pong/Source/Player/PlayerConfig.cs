namespace Samples.Pong;

readonly struct PlayerConfig
{
    public readonly PlayerIndex Index;
    public readonly Vector2 StartingPosition;
    public readonly Func<Moveable, int> MoveDirection;
    public readonly Ball Ball;

    public PlayerConfig(PlayerIndex index, float start, Func<Moveable, int> checkMove, Ball ball = null)
    {
        Index = index;
        StartingPosition = new(start, 0f);
        MoveDirection = checkMove;
        Ball = ball;
    }

    public PlayerConfig With(Ball ball) => new(Index, StartingPosition.X, MoveDirection, ball);
}