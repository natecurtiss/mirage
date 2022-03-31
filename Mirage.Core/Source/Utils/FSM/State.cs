namespace Mirage.Utils.FSM;

/// <summary>
/// A <see cref="State{T}"/> used in a <see cref="FiniteStateMachine{T}"/>.
/// </summary>
/// <typeparam name="T">The type of <see cref="FiniteStateMachine{T}"/> to use this <see cref="State{T}"/> with.</typeparam>
public interface State<T>
{
    /// <summary>
    /// Called when the <see cref="FiniteStateMachine{T}"/> is created.
    /// </summary>
    /// <param name="fsm">The <see cref="FiniteStateMachine{T}"/> the <see cref="State{T}"/> belongs to.</param>
    void Init(FiniteStateMachine<T> fsm);
    
    /// <summary>
    /// Called when the <see cref="State{T}"/> is entered in the <see cref="FiniteStateMachine{T}"/>.
    /// </summary>
    void Enter();
    
    /// <summary>
    /// Called every frame when the <see cref="State{T}"/> is the current <see cref="State{T}"/> in the <see cref="FiniteStateMachine{T}"/>.
    /// </summary>
    /// <param name="deltaTime">The time since the last frame.</param>
    void Update(float deltaTime);
    
    /// <summary>
    /// Called when the <see cref="State{T}"/> is exited in the <see cref="FiniteStateMachine{T}"/>.
    /// </summary>
    void Exit();
}