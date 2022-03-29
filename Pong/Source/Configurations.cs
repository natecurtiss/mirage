using System;
using Guap.Input;

namespace Pong;

static class Configurations
{
    static readonly float _spread = 620f;
    static readonly float _speed = 500f;

    public static readonly PlayerConfig PlayerOne = new(PlayerIndex.One, -_spread, _speed, 0.1f, (keyboard, _, _) =>
    {
        if (keyboard.IsDown(Key.W))
            return keyboard.IsUp(Key.S) ? 1 : 0;
        if (keyboard.IsDown(Key.S))
            return -1;
        return 0;
    }, (keyboard, timer) => timer.IsDone && keyboard.WasPressed(Key.Any));

    public static readonly PlayerConfig AI = new(PlayerIndex.Two, _spread, _speed, 1.5f, (_, ball, player) =>
    {
        var relative = Math.Clamp(ball.Position.Y - player.Position.Y, -1, 1);
        return (int) relative;
    }, (_, timer) => timer.IsDone);

    public static readonly BallConfig Ball = new(200f, 1.2f, 0.3f, 1f);
}