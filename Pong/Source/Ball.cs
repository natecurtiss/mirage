using System.Numerics;
using Guap;

namespace Pong;

sealed class Ball : Entity<BallOptions>
{
    readonly Random _random = new();
    BallOptions _settings;
    
    Vector2 _direction = Vector2.One;
    float _velocity;
    bool _didBounceThisFrame;
    
    protected override void OnConfigure(BallOptions settings)
    {
        _settings = settings;
        _velocity = _settings.Speed;
    }

    protected override void OnStart()
    {
        Texture = "Assets/square.png".Find();
        Scale = _settings.Scale;
    }

    protected override void OnUpdate(float dt)
    {
        Position += _direction * _velocity * dt;
        if (_didBounceThisFrame)
        {
            _didBounceThisFrame = false;
            return;
        }
        if (Bounds().Top.Y >= Window.Bounds().Top.Y || Bounds().Bottom.Y <= Window.Bounds().Bottom.Y)
        {
            _direction = new Vector2(_direction.X, -_direction.Y).Normalized();
            _didBounceThisFrame = true;
        }
    }

    public void Bounce()
    {
        var tilt = _random.Next((int) (_settings.MinBounce * 10), (int) (_settings.MaxBounce * 10)) / 10f;
        _velocity *= _settings.Multiplier;
        var dir = _direction.Y > 0 ? 1 : -1;
        _direction = new Vector2(-_direction.X, dir * tilt).Normalized() * _settings.Multiplier;
    }
}