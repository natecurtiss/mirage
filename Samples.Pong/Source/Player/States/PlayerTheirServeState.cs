using Mirage.Utils.FSM;

namespace Samples.Pong;

sealed class PlayerTheirServeState : State<PlayerState>
{
    readonly PlayerConfig _config;
    readonly Moveable _moveable;
    FiniteStateMachine<PlayerState> _fsm;

    public PlayerTheirServeState(PlayerConfig config, Moveable moveable)
    {
        _config = config;
        _moveable = moveable;
    }

    public void Init(FiniteStateMachine<PlayerState> fsm) => _fsm = fsm;

    public void Enter()
    {
        _moveable.Position = _config.StartingPosition;
        _config.Ball.OnServeEnd += OnServe;
    }

    public void Update(float deltaTime) { }

    public void Exit() => _config.Ball.OnServeEnd -= OnServe;

    void OnServe() => _fsm.SwitchTo(PlayerState.Play);
    

}