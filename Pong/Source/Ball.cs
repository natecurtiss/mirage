using System;
using System.Numerics;
using Guap;
using Guap.Utilities;
using Random = Guap.Utilities.Random;

namespace Pong;

sealed class Ball : Entity<BallOptions>
{
    public event Action<PlayerIndex> OnScore;
    public event Action OnServe;
    public event Action<PlayerIndex> OnShouldServe;

    float _startingSpeed;
    float _speedMultiplier;
    float _minBounceTilt;
    float _maxBounceTilt;
    
    float _velocity;
    Vector2 _direction;
    bool _didBounceThisFrame;
    bool _wasServed;
    
    protected override void OnConfigure(BallOptions config)
    {
        _startingSpeed = config.Speed;
        _speedMultiplier = config.SpeedMultiplier;
        _minBounceTilt = config.MinBounceTilt;
        _maxBounceTilt = config.MaxBounceTilt;
    }

    protected override void OnStart()
    {
        _velocity = _startingSpeed;
        Texture = "Assets/square.png".Find();
        Scale = new(10f);
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
            _velocity = _startingSpeed;
            OnScore?.Invoke(PlayerIndex.One);
            OnShouldServe?.Invoke(PlayerIndex.One);
        }
        else if (Position.X <= Window.Bounds().Left.X)
        {
            Position = Vector2.Zero;
            _wasServed = false;
            _velocity = _startingSpeed;
            OnScore?.Invoke(PlayerIndex.Two);
            OnShouldServe?.Invoke(PlayerIndex.Two);
        }
    }

    public void Bounce()
    {
        var tilt = Random.Between(_minBounceTilt, _maxBounceTilt);
        _velocity *= _speedMultiplier * _speedMultiplier;
        var dir = _direction.Y > 0 ? 1 : -1;
        _direction = new Vector2(-_direction.X, dir * tilt).Normalized();
    }

    public void Serve(PlayerIndex server)
    {
        OnServe?.Invoke();
        _wasServed = true;
        _direction = Vector2.One * (server == PlayerIndex.One ? 1 : -1);
    }
}