using Guap.Input;
using Guap;
using Timer = Guap.Timer;

namespace Pong;

static class Configurations
{
    static readonly float _spread = 620f;
    static readonly float _speed = 500f;
    static readonly Func<Keyboard, Timer, bool> _playerServe = (keyboard, timer) => timer.IsDone && keyboard.WasPressed(Key.Any);
    public static readonly PlayerOptions PlayerOne = new(PlayerIndex.One, -_spread, _speed, 0.1f, (keyboard, _, _) =>
    {
        if (keyboard.IsDown(Key.W))
            return keyboard.IsUp(Key.S) ? 1 : 0;
        if (keyboard.IsDown(Key.S))
            return -1;
        return 0;
    }, _playerServe);
    public static readonly PlayerOptions PlayerTwo = new(PlayerIndex.Two, _spread, _speed, 0.1f, (keyboard, _, _) =>
    {
        if (keyboard.IsDown(Key.UpArrow))
            return keyboard.IsUp(Key.DownArrow) ? 1 : 0;
        if (keyboard.IsDown(Key.DownArrow))
            return -1;
        return 0;
    }, _playerServe);
    public static readonly PlayerOptions AI = new(PlayerIndex.Two, _spread, _speed, 1.5f, (_, ball, player) =>
    {
        var relative = Math.Clamp(ball.Position.Y - player.Position.Y, -1, 1);
        return (int) relative;
    }, (_, timer) => timer.IsDone);

    public static readonly BallOptions Ball = new(200f, 1.2f, 0.3f, 1f);
}