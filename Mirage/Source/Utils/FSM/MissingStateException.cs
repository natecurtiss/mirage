using System;

namespace Mirage.Utils.FSM;

/// <summary>
/// <see cref="Exception"/> thrown when a <see cref="State{T}"/> with the specified key does not exist in the specified <see cref="FiniteStateMachine{T}"/>.
/// </summary>
public sealed class MissingStateException : Exception
{
    public MissingStateException() { }
    public MissingStateException(string message) : base(message) { }
    public MissingStateException(string message, Exception inner) : base(message, inner) { }
}