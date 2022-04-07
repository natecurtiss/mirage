using Mirage.Utils;

namespace Samples.Pong;

sealed class Ball : Entity
{
    public event Action<PlayerIndex> OnServeStart;
    public event Action OnServeEnd;

    const float STARTING_SPEED = 400f;
    const float SPEED_MULTIPLIER = 1.05f;
    const float MIN_BOUNCE_TILT = 0.3f;
    const float MAX_BOUNCE_TILT = 1f;
    const int BOUNCE_BUFFER = 2;
    
    float _velocity;
    Vector2 _direction;
    int _bounceDelayBuffer; // Prevents getting stuck in the ceiling or floor.
    bool _wasServed;

    protected override void OnAwake()
    {
        _velocity = STARTING_SPEED;
        Texture = "Assets/square.png".Find();
        Size = new(10f);
    }

    protected override void OnStart() => OnServeStart?.Invoke(PlayerIndex.One);

    protected override void OnUpdate(float deltaTime)
    {
        if (!_wasServed)
            return;
        Position += _direction * _velocity * deltaTime;
        if (_bounceDelayBuffer != 0)
        {
            _bounceDelayBuffer--;
            return;
        }
        if (Bounds.IsAbove(Window.Bounds) || Bounds.IsBelow(Window.Bounds))
        {
            _direction = new Vector2(_direction.X, -_direction.Y).Normalized();
            _bounceDelayBuffer = BOUNCE_BUFFER;
        }

        if (Bounds.IsCompletelyRightOf(Window.Bounds))
        {
            Position = Vector2.Zero;
            _wasServed = false;
            _velocity = STARTING_SPEED;
            OnServeStart?.Invoke(PlayerIndex.One);
        }
        else if (Bounds.IsCompletelyLeftOf(Window.Bounds))
        {
            Position = Vector2.Zero;
            _wasServed = false;
            _velocity = STARTING_SPEED;
            OnServeStart?.Invoke(PlayerIndex.Two);
        }
    }

    public void Hit()
    {
        var tilt = RandomNumber.Between(MIN_BOUNCE_TILT, MAX_BOUNCE_TILT);
        var up = _direction.Y > 0 ? 1 : -1;
        _velocity *= SPEED_MULTIPLIER * SPEED_MULTIPLIER;
        _direction = new Vector2(-_direction.X, up * tilt).Normalized();
        _bounceDelayBuffer = BOUNCE_BUFFER;
    }

    public void Serve(PlayerIndex server)
    {
        var tilt = RandomNumber.Between(-1f, 1f);
        var dir = server == PlayerIndex.One ? 1 : -1;
        _velocity = STARTING_SPEED;
        _direction = new Vector2(dir, dir * tilt).Normalized();
        _wasServed = true;
        _bounceDelayBuffer = 0;
        OnServeEnd?.Invoke();
    }
}