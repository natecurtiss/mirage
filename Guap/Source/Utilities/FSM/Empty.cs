namespace Guap.Utilities.FSM;

sealed class Empty<T> : State<T>
{
    public static readonly Empty<T> State = new();
    bool State<T>.IsOn => true;
    void State<T>.Initialize(FiniteStateMachine<T> fsm) { }
    void State<T>.Enter() { }
    void State<T>.Update() { }
    void State<T>.Exit() { }
}