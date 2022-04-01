using Mirage.Utils.FSM;

namespace Samples.FlappyBird;

sealed class TransitionNothingState : State<TransitionState>
{
    public void Init(FiniteStateMachine<TransitionState> fsm) { }
    public void Enter() { }
    public void Update(float deltaTime) { }
    public void Exit() { }
}