using Mirage.Utils;

namespace Samples.FlappyBird;

sealed class Transition : Entity
{
    public event Action OnCover;
    public event Action OnFinish;
    
    const float DURATION = 0.7f;
    const float DELAY = 0.3f;

    float _timer;
    float _move;
    bool _isDoing;
    bool _hasCovered;
    bool _isWaiting;
    Vector2 _startingPosition;

    protected override void OnAwake()
    {
        _move = Size.Y / 2f + Window.Height + Size.Y / 2f;
        _startingPosition = new(0f, Window.Height / 2f + Size.Y / 2f);
    }

    protected override void OnStart()
    {
        Texture = "Assets/fg_transition.png".Find();
        SortingOrder = 99;
        Size = new(1280, 720);
        Position = _startingPosition;
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (!_isDoing)
            return;
        if (!_isWaiting)
            Position -= new Vector2(0f, _move * deltaTime * (1 / DURATION));
        if (_isWaiting)
        {
            _timer -= deltaTime;
            if (_timer <= 0f)
            {
                _isWaiting = false;
                _hasCovered = true;
                OnCover?.Invoke();
            }
        }
        else if (Position.Y <= 0f && !_hasCovered)
        {
            _isWaiting = true;
            _timer = DELAY;
            Position = new(0f);
        }
        else if (Position.Y <= _startingPosition.Y - _move)
        {
            OnFinish?.Invoke();
            Position = _startingPosition;
            _isDoing = false;
        }
    }

    public void Do()
    {
        Position = _startingPosition;
        _isDoing = true;
        _hasCovered = false;
        _isWaiting = false;
    }
}