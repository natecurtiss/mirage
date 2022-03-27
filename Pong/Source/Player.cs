using System.Numerics;
using Guap;

namespace Pong;

sealed class Player : Entity<PlayerOptions>
{
    PlayerOptions _options;

    protected override void OnConfigure(PlayerOptions settings) => _options = settings;

    protected override void OnStart()
    {
        Texture = "Assets/square.png".Find();
        Scale = new(10f, 100f);
        Position = _options.Start;
    }

    protected override void OnUpdate(float dt)
    {
        Position += new Vector2(0f, _options.MoveDirection(Keyboard, _options.Ball, this) * _options.Speed * dt);
        var top = Window.Bounds().Top.Y - Bounds().Extents.Y;
        var bottom = Window.Bounds().Bottom.Y + Bounds().Extents.Y;
        Position = new(Position.X, Math.Clamp(Position.Y, bottom, top));
        if (Bounds().Contains(_options.Ball.Bounds()))
            _options.Ball.Bounce();
    }
}