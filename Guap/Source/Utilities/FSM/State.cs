namespace Guap.Utilities.FSM;

public interface State<T>
{
    void Initialize(FiniteStateMachine<T> fsm);
    void Enter();
    void Update(float dt);
    void Exit();
}