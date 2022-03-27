using System.Numerics;
using Guap;
using Timer = Guap.Timer;

namespace Pong;

sealed class Player : Entity<PlayerOptions>
{
    readonly Timer _timer = new(0f);
    PlayerOptions _options;
    bool _shouldServe;
    bool _someoneIsServing;

    protected override void OnConfigure(PlayerOptions settings)
    {
        _options = settings;
        _timer.Remaining = _options.ServeDelay;
    }

    protected override void OnStart()
    {
        Texture = "Assets/square.png".Find();
        Scale = new(10f, 100f);
        Position = _options.Start;
    }

    protected override void OnUpdate(float dt)
    {
        _timer.Tick(dt);
        if (_shouldServe)
        {
            if (_options.ShouldServe(Keyboard, _timer))
            {
                _options.Ball.Serve(_options.Number);
                _shouldServe = false;
            }
        }
        else if (!_someoneIsServing)
        {
            Position += new Vector2(0f, _options.MoveDirection(Keyboard, _options.Ball, this) * _options.Speed * dt);
            var top = Window.Bounds().Top.Y - Bounds().Extents.Y;
            var bottom = Window.Bounds().Bottom.Y + Bounds().Extents.Y;
            Position = new(Position.X, Math.Clamp(Position.Y, bottom, top));
            if (Bounds().Contains(_options.Ball.Bounds()))
                _options.Ball.Bounce();
        }
    }

    public void WaitForServe(PlayerNumber server)
    {
        _someoneIsServing = true;
        Position = _options.Start;
        if (_options.Number != server)
            return;
        _timer.Reset();
        _shouldServe = true;
    }

    public void SomeoneServed() => _someoneIsServing = false;
}