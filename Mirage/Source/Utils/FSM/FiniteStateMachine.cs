using System.Collections.Generic;

namespace Mirage.Utils.FSM;

/// <summary>
/// A "machine" that can be in exactly one of a finite amount of <see cref="State{T}">States.</see>
/// </summary>
/// <typeparam name="T">The type of key to use to identify <see cref="State{T}">States.</see></typeparam>
public sealed class FiniteStateMachine<T>
{
    readonly Dictionary<T, State<T>> _states = new();
    State<T> _current = Empty<T>.State;

    State<T> this[T index]
    {
        get
        {
            if (!_states.ContainsKey(index))
                throw new MissingStateException($"State of type {index} does not exist in Finite State Machine of type {this}!");
            return _states[index];
        }
    }

    /// <summary>
    /// Creates a new <see cref="FiniteStateMachine{T}"/>.
    /// </summary>
    /// <param name="first">The initial <see cref="State{T}"/> of the <see cref="FiniteStateMachine{T}"/>.</param>
    /// <param name="states">All of the <see cref="State{T}">States</see> and their keys in the <see cref="FiniteStateMachine{T}"/>.</param>
    public FiniteStateMachine(T first, params (T, State<T>)[] states)
    {
        foreach (var (key, value) in states) 
            _states.Add(key, value);
        foreach (var (_, value) in _states) 
            value.Init(this);
        SwitchTo(first);
    }

    /// <summary>
    /// Switches the <see cref="FiniteStateMachine{T}"/>'s <see cref="State{T}"/> to the <see cref="State{T}"/> with the key of T.
    /// </summary>
    /// <param name="key">The key of the <see cref="State{T}"/> to switch to.</param>
    public void SwitchTo(T key) => SwitchTo(this[key]);

    /// <summary>
    /// Switches the <see cref="FiniteStateMachine{T}"/>'s <see cref="State{T}"/> to the specified <see cref="State{T}"/>.
    /// </summary>
    /// <param name="state">The <see cref="State{T}"/> to switch to.</param>
    public void SwitchTo(State<T> state)
    {
        _current.Exit();
        _current = state ?? Empty<T>.State;
        _current.Enter();
    }

    /// <summary>
    /// Updates the current <see cref="State{T}"/> in the <see cref="FiniteStateMachine{T}"/>.
    /// </summary>
    /// <param name="deltaTime">The time since the last frame.</param>
    public void Update(float deltaTime) => _current.Update(deltaTime);
}