using Mirage.Utils.FSM;

namespace Samples.FlappyBird;

sealed class TransitionCoveringState : State<TransitionState>
{
    readonly Action _onEnter;
    readonly Action _onExit;
    readonly float _delay;

    FiniteStateMachine<TransitionState> _fsm;
    float _timer;

    public TransitionCoveringState(Action onEnter, Action onExit, float delay)
    {
        _onEnter = onEnter;
        _onExit = onExit;
        _delay = delay;
    }

    public void Init(FiniteStateMachine<TransitionState> fsm) => _fsm = fsm;

    public void Enter()
    {
        _timer = _delay;
        _onEnter();
    }

    public void Update(float deltaTime)
    {
        _timer -= deltaTime;
        if (_timer <= 0f)
            _fsm.SwitchTo(TransitionState.Out);
    }

    public void Exit() => _onExit();
}