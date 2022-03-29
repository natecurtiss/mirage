using Guap;
using Guap.Input;
using Guap.Utilities.FSM;

namespace Pong;

sealed class PlayerMyServeState : State<PlayerState>
{
    readonly PlayerConfig _config;
    readonly Moveable _moveable;
    readonly Keyboard _keyboard;
    readonly Timer _timer;
    FiniteStateMachine<PlayerState> _fsm;

    public PlayerMyServeState(PlayerConfig config, Moveable moveable, Keyboard keyboard)
    {
        _config = config;
        _moveable = moveable;
        _keyboard = keyboard;
        _timer = new(_config.ServeDelay);
    }
    
    void State<PlayerState>.Init(FiniteStateMachine<PlayerState> fsm) => _fsm = fsm;

    void State<PlayerState>.Enter()
    {
        _timer.Reset();
        _moveable.Position = _config.StartingPosition;
    }

    void State<PlayerState>.Update(float dt)
    {
        _timer.Tick(dt);
        if (_config.ShouldServe(_keyboard, _timer))
        {
            _config.Ball.Serve(_config.Index);
            _fsm.SwitchTo(PlayerState.Play);
        }
    }

    void State<PlayerState>.Exit() { }
}