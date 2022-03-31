namespace Mirage.Utils.FSM;

/// <summary>
/// An empty <see cref="State{T}"/>.
/// </summary>
/// <remarks>Use <see cref="State">Empty&lt;T&gt;.State </see>to get an empty <see cref="State{T}"/></remarks>
/// <inheritdoc cref="State{T}"/>
sealed class Empty<T> : State<T>
{
    /// <summary>
    /// An instance of an empty <see cref="State{T}"/>.
    /// </summary>
    public static readonly Empty<T> State = new();
    
    Empty() { }
    
    /// <inheritdoc />
    void State<T>.Init(FiniteStateMachine<T> fsm) { }
    
    /// <inheritdoc />
    void State<T>.Enter() { }
    
    /// <inheritdoc />
    void State<T>.Update(float deltaTime) { }
    
    /// <inheritdoc />
    void State<T>.Exit() { }
}