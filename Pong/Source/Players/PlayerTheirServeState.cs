using System.Numerics;
using Guap;
using Guap.Utilities.FSM;

namespace Pong.Players;

sealed class PlayerTheirServeState : State<PlayerState>
{
    readonly Entity _entity;
    readonly Vector2 _startingPosition;

    public PlayerTheirServeState(Entity entity, Vector2 startingPosition)
    {
        _entity = entity;
        _startingPosition = startingPosition;
    }
    
    void State<PlayerState>.Initialize(FiniteStateMachine<PlayerState> fsm) { }
    void State<PlayerState>.Enter() => _entity.Position = _startingPosition;
    void State<PlayerState>.Update(float dt) { }
    void State<PlayerState>.Exit() { }
}