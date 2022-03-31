﻿using Mirage.Input;
using Mirage.Utils.FSM;

namespace Samples.Pong;

sealed class MyServe : State<PlayerState>
{
    readonly PlayerConfig _config;
    readonly Moveable _moveable;
    readonly Keyboard _keyboard;
    FiniteStateMachine<PlayerState> _fsm;

    public MyServe(PlayerConfig config, Moveable moveable, Keyboard keyboard)
    {
        _config = config;
        _moveable = moveable;
        _keyboard = keyboard;
    }
    
    void State<PlayerState>.Init(FiniteStateMachine<PlayerState> fsm) => _fsm = fsm;

    void State<PlayerState>.Enter() => _moveable.Position = _config.StartingPosition;

    void State<PlayerState>.Update(float deltaTime)
    {
        if (_keyboard.IsDown(Key.Space))
        {
            _config.Ball.Serve(_config.Index);
            _fsm.SwitchTo(PlayerState.Play);
        }
    }

    void State<PlayerState>.Exit() { }
}