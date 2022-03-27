using System.Collections.Generic;

namespace Guap.Utilities.FSM;

public sealed class FiniteStateMachine<T>
{
    readonly Dictionary<T, State<T>> _states = new();
    State<T> _current = Empty<T>.State;

    public State<T> this[T index] => _states[index];

    public FiniteStateMachine(params (T, State<T>)[] states)
    {
        foreach (var (key, value) in states) 
            _states.Add(key, value);
    }

    public void SwitchTo(State<T> enter)
    {
        _current.Exit();
        _current = enter ?? Empty<T>.State;
        _current.Enter();
    }

    public void Update() => _current.Update();
}