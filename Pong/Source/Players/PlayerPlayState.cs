using System;
using System.Numerics;
using Guap;
using Guap.Input;
using Guap.Utilities.FSM;

namespace Pong.Players;

sealed class PlayerPlayState : State<PlayerState>
{
    readonly Func<Keyboard, Ball, Entity, int> _directionToMove;
    readonly Entity _entity;
    readonly Ball _ball;
    readonly Window _window;
    readonly Keyboard _keyboard;
    readonly float _moveSpeed;
    FiniteStateMachine<PlayerState> _fsm;

    public PlayerPlayState(Func<Keyboard, Ball, Entity, int> directionToMove, Entity entity, Ball ball, Window window, Keyboard keyboard, float moveSpeed)
    {
        _directionToMove = directionToMove;
        _entity = entity;
        _ball = ball;
        _window = window;
        _keyboard = keyboard;
        _moveSpeed = moveSpeed;
    }

    void State<PlayerState>.Initialize(FiniteStateMachine<PlayerState> fsm) => _fsm = fsm;

    void State<PlayerState>.Enter()
    {
        
    }

    void State<PlayerState>.Update(float dt)
    {
        _entity.Position += new Vector2(0f, _directionToMove(_keyboard, _ball, _entity) * _moveSpeed * dt);
        var top = _window.Bounds().Top.Y - _entity.Bounds().Extents.Y;
        var bottom = _window.Bounds().Bottom.Y + _entity.Bounds().Extents.Y;
        _entity.Position = new(_entity.Position.X, Math.Clamp(_entity.Position.Y, bottom, top));
        if (_entity.Bounds().Contains(_ball.Bounds()))
            _ball.Bounce();
    }

    void State<PlayerState>.Exit()
    {
        
    }
}