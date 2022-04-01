namespace Samples.FlappyBird;

sealed class TransitionInState : TransitionMoveState
{
    protected override Moveable Moveable { get; }
    protected override float Distance { get; }
    protected override float Duration { get; }

    readonly float _startingPos;
    
    public TransitionInState(Moveable moveable, float startingPos, float distance, float duration)
    {
        Moveable = moveable;
        _startingPos = startingPos;
        Distance = distance;
        Duration = duration;
    }

    public override void Enter() => Moveable.Position = new(0f, _startingPos);

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (Moveable.Position.Y <= 0f)
        {
            Moveable.Position = new(0f);
            FSM.SwitchTo(TransitionState.Covering);
        }
    }
}