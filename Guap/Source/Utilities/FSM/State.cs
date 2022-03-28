namespace Guap.Utilities.FSM;

public interface State<T>
{
    void Init(FiniteStateMachine<T> fsm);
    void Enter();
    void Update(float dt);
    void Exit();
}