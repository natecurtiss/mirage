using System.Numerics;
using Guap;
using Guap.Input;
using Timer = Guap.Timer;

namespace Pong;

sealed class Player : Entity<PlayerOptions>
{
    Timer _timer;
    Ball _ball;
    Vector2 _startingPosition;
    float _moveSpeed;
    PlayerIndex _index;
    Func<Keyboard, Ball, Player, int> _directionToMove;
    Func<Keyboard, Timer, bool> _canServe;

    bool _shouldServe;
    bool _someoneIsServing;

    protected override void OnConfigure(PlayerOptions config)
    {
        _timer = new(config.ServeDelay);
        _ball = config.Ball;
        _startingPosition = config.StartingPosition;
        _moveSpeed = config.Speed;
        _index = config.Index;
        _directionToMove = config.MoveDirection;
        _canServe = config.ShouldServe;
    }

    protected override void OnStart()
    {
        Texture = "Assets/square.png".Find();
        Scale = new(10f, 100f);
        Position = _startingPosition;
    }

    protected override void OnUpdate(float dt)
    {
        _timer.Tick(dt);
        if (_shouldServe)
        {
            if (_canServe(Keyboard, _timer))
            {
                _ball.Serve(_index);
                _shouldServe = false;
            }
        }
        else if (!_someoneIsServing)
        {
            Position += new Vector2(0f, _directionToMove(Keyboard, _ball, this) * _moveSpeed * dt);
            var top = Window.Bounds().Top.Y - Bounds().Extents.Y;
            var bottom = Window.Bounds().Bottom.Y + Bounds().Extents.Y;
            Position = new(Position.X, Math.Clamp(Position.Y, bottom, top));
            if (Bounds().Contains(_ball.Bounds()))
                _ball.Bounce();
        }
    }

    public void WaitForServe(PlayerIndex server)
    {
        _someoneIsServing = true;
        Position = _startingPosition;
        if (_index != server)
            return;
        _timer.Reset();
        _shouldServe = true;
    }

    public void SomeoneServed() => _someoneIsServing = false;
}