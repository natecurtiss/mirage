using Guap;
using Guap.Utilities.FSM;

namespace Pong;

sealed class PlayerTheirServeState : State<PlayerState>
{
    readonly PlayerVariables _config;
    readonly Moveable _moveable;
    FiniteStateMachine<PlayerState> _fsm;

    public PlayerTheirServeState(PlayerVariables config, Moveable moveable)
    {
        _config = config;
        _moveable = moveable;
    }

    void State<PlayerState>.Init(FiniteStateMachine<PlayerState> fsm) => _fsm = fsm;

    void State<PlayerState>.Enter()
    {
        _moveable.Position = _config.StartingPosition;
        _config.Ball.OnServeEnd += OnServe;
    }

    void State<PlayerState>.Update(float dt) { }

    void State<PlayerState>.Exit() => _config.Ball.OnServeEnd -= OnServe;

    void OnServe() => _fsm.SwitchTo(PlayerState.Play);
    

}