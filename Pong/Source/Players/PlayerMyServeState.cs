using System;
using System.Numerics;
using Guap;
using Guap.Input;
using Guap.Utilities.FSM;

namespace Pong.Players;

sealed class PlayerMyServeState : State<PlayerState>
{
    readonly Func<Keyboard, Timer, bool> _canServe;
    readonly PlayerIndex _index;
    readonly Entity _entity;
    readonly Vector2 _startingPosition;
    readonly Timer _timer;
    readonly Keyboard _keyboard;
    readonly Ball _ball;
    FiniteStateMachine<PlayerState> _fsm;

    public PlayerMyServeState(PlayerIndex index, Entity entity, Vector2 startingPosition, Keyboard keyboard, Ball ball, float serveDelay, Func<Keyboard, Timer, bool> canServe)
    {
        _index = index;
        _entity = entity;
        _startingPosition = startingPosition;
        _keyboard = keyboard;
        _ball = ball;
        _timer = new(serveDelay);
        _canServe = canServe;
    }
    
    void State<PlayerState>.Initialize(FiniteStateMachine<PlayerState> fsm) => _fsm = fsm;

    void State<PlayerState>.Enter()
    {
        _timer.Reset();
        _entity.Position = _startingPosition;
    }

    void State<PlayerState>.Update(float dt)
    {
        _timer.Tick(dt);
        if (_canServe(_keyboard, _timer))
        {
            _ball.Serve(_index);
            _fsm.SwitchTo(PlayerState.Play);
        }
    }

    void State<PlayerState>.Exit() { }
}