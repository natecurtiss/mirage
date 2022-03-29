using System;
using Guap;
using Guap.Input;

namespace Pong;

static class Configurations
{
    static readonly float _spread = 620f;
    static readonly float _speed = 500f;
    static readonly Func<Keyboard, bool> _serve = keyboard =>keyboard.WasPressed(Key.Any);

    public static readonly PlayerConfig PlayerOne = new(PlayerIndex.One, -_spread, _speed, (keyboard, _, _) =>
    {
        if (keyboard.IsDown(Key.W) || keyboard.IsDown(Key.UpArrow))
            return keyboard.IsUp(Key.S) || keyboard.IsUp(Key.DownArrow) ? 1 : 0;
        if (keyboard.IsDown(Key.S) || keyboard.IsDown(Key.DownArrow))
            return -1;
        return 0;
    }, _serve);

    public static readonly PlayerConfig AI = new(PlayerIndex.Two, _spread, _speed, (_, ball, player) =>
    {
        var relative = Math.Clamp(ball.Position.Y - player.Position.Y, -1, 1);
        return (int) relative;
    }, _serve);

    public static readonly BallConfig Ball = new(200f, 1.2f, 0.3f, 1f);
}