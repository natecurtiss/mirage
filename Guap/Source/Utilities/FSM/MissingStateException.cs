using System;

namespace Guap.Utilities.FSM;

public sealed class MissingStateException : Exception
{
    public MissingStateException() { }
    public MissingStateException(string message) : base(message) { }
    public MissingStateException(string message, Exception inner) : base(message, inner) { }
}