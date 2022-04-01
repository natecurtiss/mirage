using Mirage.Input;
using Mirage.Utils.FSM;

namespace Samples.Pong;

sealed class PlayerMyServeState : State<PlayerState>
{
    readonly PlayerConfig _config;
    readonly Moveable _moveable;
    readonly Keyboard _keyboard;
    FiniteStateMachine<PlayerState> _fsm;

    public PlayerMyServeState(PlayerConfig config, Moveable moveable, Keyboard keyboard)
    {
        _config = config;
        _moveable = moveable;
        _keyboard = keyboard;
    }
    
    public void Init(FiniteStateMachine<PlayerState> fsm) => _fsm = fsm;

    public void Enter() => _moveable.Position = _config.StartingPosition;

    public void Update(float deltaTime)
    {
        if (_keyboard.IsDown(Key.Space))
        {
            _config.Ball.Serve(_config.Index);
            _fsm.SwitchTo(PlayerState.Play);
        }
    }

    public void Exit() { }
}