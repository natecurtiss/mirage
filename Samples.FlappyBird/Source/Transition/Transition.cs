using Mirage.Utils;
using Mirage.Utils.FSM;

namespace Samples.FlappyBird;

sealed class Transition : Entity
{
    public event Action OnCover;
    public event Action OnFinish;
    
    const float DURATION = 0.7f;
    const float DELAY = 0.3f;

    FiniteStateMachine<TransitionState> _fsm;

    protected override void OnAwake()
    {
        Texture = "Assets/fg_transition.png".Find();
        SortingOrder = 99;
        Size = new(1280, 720);
        var startingPos = new Vector2(0f, Window.Height / 2f + Size.Y / 2f);
        var dist = Size.Y / 2f + Window.Height + Size.Y / 2f;
        _fsm = new(TransitionState.Nothing,
            (TransitionState.Nothing, new TransitionNothingState()),
            (TransitionState.In, new TransitionInState(this, startingPos.Y, dist, DURATION)),
            (TransitionState.Covering, new TransitionCoveringState(() => OnCover?.Invoke(), () => OnFinish?.Invoke(), DELAY)),
            (TransitionState.Out, new TransitionOutState(this, startingPos.Y, dist, DURATION))
        );
        Position = startingPos;
    }

    protected override void OnUpdate(float deltaTime) => _fsm.Update(deltaTime);

    public void Do() => _fsm.SwitchTo(TransitionState.In);
}