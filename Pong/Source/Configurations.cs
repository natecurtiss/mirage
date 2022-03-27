using Guap.Input;

namespace Pong;

static class Configurations
{
    static readonly float _spread = 620f;
    static readonly float _speed = 500f;
    public static readonly PlayerOptions PlayerOne = new(-_spread, _speed, (keyboard, _, _) =>
    {
        if (keyboard.IsDown(Key.W))
            return keyboard.IsUp(Key.S) ? 1 : 0;
        if (keyboard.IsDown(Key.S))
            return -1;
        return 0;
    });
    public static readonly PlayerOptions PlayerTwo = new(_spread, _speed, (keyboard, _, _) =>
    {
        if (keyboard.IsDown(Key.UpArrow))
            return keyboard.IsUp(Key.DownArrow) ? 1 : 0;
        if (keyboard.IsDown(Key.DownArrow))
            return -1;
        return 0;
    });
    public static readonly PlayerOptions AI = new(_spread, _speed, (_, ball, player) =>
    {
        var relative = Math.Clamp(ball.Position.Y - player.Position.Y, -1, 1);
        return (int) relative;
    });

    public static readonly BallOptions Ball = new(10f);
}