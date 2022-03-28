namespace Guap.Utilities.FSM;

sealed class Empty<T> : State<T>
{
    public static readonly Empty<T> State = new();
    void State<T>.Initialize(FiniteStateMachine<T> fsm) { }
    void State<T>.Enter() { }
    void State<T>.Update(float dt) { }
    void State<T>.Exit() { }
}