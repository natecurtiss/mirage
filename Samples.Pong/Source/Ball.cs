using Mirage.Utils;

namespace Samples.Pong;

sealed class Ball : Entity<BallConfig>
{
    public event Action<PlayerIndex> OnServeStart;
    public event Action OnServeEnd;

    float _startingSpeed;
    float _speedMultiplier;
    float _minBounceTilt;
    float _maxBounceTilt;
    
    float _velocity;
    Vector2 _direction;
    bool _didBounceThisFrame;
    bool _wasServed;
    
    protected override void OnConfigure(BallConfig config)
    {
        _startingSpeed = config.Speed;
        _speedMultiplier = config.SpeedMultiplier;
        _minBounceTilt = config.MinBounceTilt;
        _maxBounceTilt = config.MaxBounceTilt;
    }

    protected override void OnAwake()
    {
        _velocity = _startingSpeed;
        Texture = "Assets/square.png".Find();
        Size = new(10f);
    }

    protected override void OnStart() => OnServeStart?.Invoke(PlayerIndex.One);

    protected override void OnUpdate(float deltaTime)
    {
        if (!_wasServed)
            return;
        Position += _direction * _velocity * deltaTime;
        if (_didBounceThisFrame)
        {
            _didBounceThisFrame = false;
            return;
        }
        if (Bounds.IsAbove(Window.Bounds) || Bounds.IsBelow(Window.Bounds))
        {
            _direction = new Vector2(_direction.X, -_direction.Y).Normalized();
            _didBounceThisFrame = true;
        }

        if (Bounds.IsCompletelyRightOf(Window.Bounds))
        {
            Position = Vector2.Zero;
            _wasServed = false;
            _velocity = _startingSpeed;
            OnServeStart?.Invoke(PlayerIndex.One);
        }
        else if (Bounds.IsCompletelyLeftOf(Window.Bounds))
        {
            Position = Vector2.Zero;
            _wasServed = false;
            _velocity = _startingSpeed;
            OnServeStart?.Invoke(PlayerIndex.Two);
        }
    }

    public void Bounce()
    {
        var tilt = RandomNumber.Between(_minBounceTilt, _maxBounceTilt);
        var up = _direction.Y > 0 ? 1 : -1;
        _velocity *= _speedMultiplier * _speedMultiplier;
        _direction = new Vector2(-_direction.X, up * tilt).Normalized();
    }

    public void Serve(PlayerIndex server)
    {
        var tilt = RandomNumber.Between(-1f, 1f);
        var dir = server == PlayerIndex.One ? 1 : -1;
        _direction = new Vector2(dir, dir * tilt).Normalized();
        _wasServed = true;
        OnServeEnd?.Invoke();
    }
}