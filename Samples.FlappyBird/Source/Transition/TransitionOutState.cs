namespace Samples.FlappyBird;

sealed class TransitionOutState : TransitionMoveState
{
    protected override Moveable Moveable { get; }
    protected override float Distance { get; }
    protected override float Duration { get; }

    readonly float _startingPos;

    public TransitionOutState(Moveable moveable, float startingPos, float distance, float duration)
    {
        Moveable = moveable;
        _startingPos = startingPos;
        Distance = distance;
        Duration = duration;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (Moveable.Position.Y <= _startingPos - Distance)
        {
            Moveable.Position = new(_startingPos);
            FSM.SwitchTo(TransitionState.Nothing);
        }
    }
}