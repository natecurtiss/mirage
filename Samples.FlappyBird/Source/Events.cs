using Mirage.Input;

namespace Samples.FlappyBird;

sealed class Events : Entity
{
    public new event Action OnStart;
    public event Action OnReset;

    bool _hasStarted;

    protected override void OnUpdate(float deltaTime)
    {
        if (_hasStarted)
            return;
        if (Keyboard.WasPressed(Key.Space))
        {
            _hasStarted = true;
            OnStart?.Invoke();
        }
    }

    public void Reset()
    {
        _hasStarted = false;
        OnReset?.Invoke();
    }
}