using System.Numerics;
using Guap;
using Math = System.Math;

namespace Pong;

sealed class Player : Entity<PlayerOptions>
{
    readonly float _speed = 500f;
    PlayerOptions _options;

    protected override void OnConfigure(PlayerOptions settings) => _options = settings;

    protected override void OnStart()
    {
        Texture = "Assets/square.png".Find();
        Scale = new(10f, 100f);
        Position = _options.StartingPosition;
    }

    protected override void OnUpdate(float dt)
    {
        if (Keyboard.IsDown(_options.Up))
        {
            if (Keyboard.IsUp(_options.Down))
                Position += new Vector2(0f, _speed * dt);
        }
        else if (Keyboard.IsDown(_options.Down))
        {
            Position -= new Vector2(0f, _speed * dt);
        }
        Position = new(Position.X, Math.Clamp(Position.Y, Window.Bounds().Bottom.Y + Bounds().Extents.Y, Window.Bounds().Top.Y - Bounds().Extents.Y));
    }
}