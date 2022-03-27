using System;

namespace Guap.Utilities.FSM;

public interface State<T>
{
    bool IsOn { get; }
    
    void Initialize(FiniteStateMachine<T> fsm);
    void Enter();
    void Update();
    void Exit();
}