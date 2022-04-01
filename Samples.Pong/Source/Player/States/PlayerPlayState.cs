using Mirage.Utils.FSM;

namespace Samples.Pong;

sealed class PlayerPlayState : State<PlayerState>
{
    const float SPEED = 500f;
    
    readonly PlayerConfig _config;
    readonly Moveable _moveable;
    readonly Boundable _boundable;
    readonly Boundable _window;
    FiniteStateMachine<PlayerState> _fsm;

    public PlayerPlayState(PlayerConfig config, Moveable moveable, Boundable boundable, Boundable window)
    {
        _config = config;
        _moveable = moveable;
        _boundable = boundable;
        _window = window;
    }
    
    public void Init(FiniteStateMachine<PlayerState> fsm) => _fsm = fsm;

    public void Enter() => _config.Ball.OnServeStart += OnScore;

    public void Update(float deltaTime)
    {
        _moveable.Position += new Vector2(0f, _config.MoveDirection(_moveable) * SPEED * deltaTime);
        var top = _window.Bounds.Top.Y - _boundable.Bounds.Extents.Y;
        var bottom = _window.Bounds.Bottom.Y + _boundable.Bounds.Extents.Y;
        _moveable.Position = new(_moveable.Position.X, Math.Clamp(_moveable.Position.Y, bottom, top));
        if (_boundable.Bounds.Overlaps(_config.Ball.Bounds))
            _config.Ball.Hit();
    }

    public void Exit() => _config.Ball.OnServeStart -= OnScore;

    void OnScore(PlayerIndex server) => _fsm.SwitchTo(_config.Index == server ? PlayerState.MyServe : PlayerState.TheirServe);
}