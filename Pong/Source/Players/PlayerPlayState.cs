using System;
using System.Numerics;
using Guap;
using Guap.Utilities.FSM;

namespace Pong;

sealed class PlayerPlayState : State<PlayerState>
{
    readonly PlayerVariables _config;
    readonly Moveable _moveable;
    readonly Boundable _boundable;
    readonly Modules _modules;
    FiniteStateMachine<PlayerState> _fsm;

    public PlayerPlayState(PlayerVariables config, Moveable moveable, Boundable boundable, Modules modules)
    {
        _config = config;
        _moveable = moveable;
        _boundable = boundable;
        _modules = modules;
    }

    // TODO: Maybe move this to the constructor.
    void State<PlayerState>.Init(FiniteStateMachine<PlayerState> fsm) => _fsm = fsm;

    void State<PlayerState>.Enter() => _config.Ball.OnShouldServe += OnShouldServe;

    void State<PlayerState>.Update(float dt)
    {
        _moveable.Position += new Vector2(0f, _config.MoveDirection(_modules.Keyboard, _config.Ball, _moveable) * _config.Speed * dt);
        var top = _modules.Window.Bounds().Top.Y - _boundable.Bounds().Extents.Y;
        var bottom = _modules.Window.Bounds().Bottom.Y + _boundable.Bounds().Extents.Y;
        _moveable.Position = new(_moveable.Position.X, Math.Clamp(_moveable.Position.Y, bottom, top));
        if (_boundable.Bounds().Contains(_config.Ball.Bounds()))
            _config.Ball.Bounce();
    }

    void State<PlayerState>.Exit() => _config.Ball.OnShouldServe -= OnShouldServe;

    void OnShouldServe(PlayerIndex server) => _fsm.SwitchTo(_config.Index == server ? PlayerState.MyServe : PlayerState.TheirServe);
}