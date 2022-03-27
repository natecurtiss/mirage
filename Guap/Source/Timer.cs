using System;

namespace Guap;

public sealed class Timer
{
    public event Action OnFinish;
    public event Action<float> OnTick;
    public event Action OnReset;
    
    float _starting;
    float _remaining;

    public float Remaining
    {
        get => _remaining;
        set
        {
            _starting = Math.Max(0, value);
            _remaining = _starting;
        }
    }
    public bool IsDone => _remaining == 0;
    public bool ShouldResetOnFinish { get; set; }

    public Timer(float starting, bool shouldResetOnFinish = false)
    {
        Remaining = starting;
        ShouldResetOnFinish = shouldResetOnFinish;
    }

    public void Tick(float t)
    {
        _remaining = Math.Max(_remaining - t, 0);
        OnTick?.Invoke(t);
        
        if (_remaining == 0)
        {
            OnFinish?.Invoke();
            if (ShouldResetOnFinish) 
                Reset();
        }
    }

    public void Reset()
    {
        _remaining = _starting;
        OnReset?.Invoke();
    }
}