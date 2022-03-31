namespace Samples.Pong;

readonly struct PlayerConfig
{
    public readonly PlayerIndex Index;
    public readonly Vector2 StartingPosition;
    public readonly Func<Moveable, int> MoveDirection;
    public readonly Ball Ball;

    public PlayerConfig(PlayerIndex index, float startingX, Func<Moveable, int> input, Ball ball)
    {
        Index = index;
        StartingPosition = new(startingX, 0f);
        MoveDirection = input;
        Ball = ball;
    }

    public PlayerConfig With(Ball ball) => new(Index, StartingPosition.X, MoveDirection, ball);
}