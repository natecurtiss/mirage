using Mirage.Utils.FSM;

namespace Samples.FlappyBird;

abstract class TransitionMoveState : State<TransitionState>
{
    protected FiniteStateMachine<TransitionState> FSM;
    
    protected abstract Moveable Moveable { get; }
    protected abstract float Distance { get; }
    protected abstract float Duration { get; }

    public void Init(FiniteStateMachine<TransitionState> fsm) => FSM = fsm;
    public virtual void Enter() { }
    public virtual void Update(float deltaTime) => Moveable.Position -= new Vector2(0f, Distance * deltaTime * (1 / Duration));
    public virtual void Exit() { }
}