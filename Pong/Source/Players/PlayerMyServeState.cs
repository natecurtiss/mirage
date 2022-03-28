using Guap;
using Guap.Utilities.FSM;

namespace Pong;

sealed class PlayerMyServeState : State<PlayerState>
{
    readonly PlayerVariables _config;
    readonly Modules _modules;
    readonly Moveable _moveable;
    readonly Timer _timer;
    FiniteStateMachine<PlayerState> _fsm;

    public PlayerMyServeState(PlayerVariables config, Modules modules, Moveable moveable)
    {
        _config = config;
        _modules = modules;
        _moveable = moveable;
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
        if (_config.ShouldServe(_modules.Keyboard, _timer))
        {
            _config.Ball.Serve(_config.Index);
            _fsm.SwitchTo(PlayerState.Play);
        }
    }

    void State<PlayerState>.Exit() { }
}