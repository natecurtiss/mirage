namespace Samples.FlappyBird;

sealed class TransitionInState : TransitionMoveState
{
    protected override Moveable Moveable { get; }
    protected override float Distance { get; }
    protected override float Duration { get; }
    
    public TransitionInState(Moveable moveable, float distance, float duration)
    {
        Moveable = moveable;
        Distance = distance;
        Duration = duration;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (Moveable.Position.Y <= 0f)
        {
            Moveable.Position = new(0f);
            FSM.SwitchTo(TransitionState.Nothing);
        }
    }
}