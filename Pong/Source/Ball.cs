using System.Numerics;
using Guap;

namespace Pong;

sealed class Ball : Entity<BallOptions>
{
    public event Action<PlayerIndex> OnScore;
    public event Action OnServe;
    public event Action<PlayerIndex> OnShouldServe;
    readonly Random _random = new();
    
    BallOptions _settings;
    Vector2 _direction = Vector2.One;
    float _velocity;
    bool _didBounceThisFrame;
    bool _wasServed;
    
    protected override void OnConfigure(BallOptions config)
    {
        _settings = config;
        _velocity = _settings.Speed;
    }

    protected override void OnStart()
    {
        Texture = "Assets/square.png".Find();
        Scale = _settings.Scale;
        OnShouldServe?.Invoke(PlayerIndex.One);
    }

    protected override void OnUpdate(float dt)
    {
        if (!_wasServed)
            return;
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

        if (Position.X >= Window.Bounds().Right.X)
        {
            Position = Vector2.Zero;
            _wasServed = false;
            _velocity = _settings.Speed;
            OnScore?.Invoke(PlayerIndex.One);
            OnShouldServe?.Invoke(PlayerIndex.One);
        }
        else if (Position.X <= Window.Bounds().Left.X)
        {
            Position = Vector2.Zero;
            _wasServed = false;
            _velocity = _settings.Speed;
            OnScore?.Invoke(PlayerIndex.Two);
            OnShouldServe?.Invoke(PlayerIndex.Two);
        }
    }

    public void Bounce()
    {
        var tilt = _random.Next((int) (_settings.MinBounce * 10), (int) (_settings.MaxBounce * 10)) / 10f;
        _velocity *= _settings.Multiplier;
        var dir = _direction.Y > 0 ? 1 : -1;
        _direction = new Vector2(-_direction.X, dir * tilt).Normalized() * _settings.Multiplier;
    }

    public void Serve(PlayerIndex server)
    {
        OnServe?.Invoke();
        _wasServed = true;
        _direction = Vector2.One * (server == PlayerIndex.One ? 1 : -1);
    }
}