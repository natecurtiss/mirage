using System;
using System.Collections.Generic;
using System.Linq;
using static Mirage.Input.Key;
using static Mirage.Input.KeyState;

namespace Mirage.Input;

public sealed class Keyboard
{
    public event Action<Key> OnKeyPress;
    public event Action<Key> OnKeyRelease;
    
    readonly Dictionary<Key, KeyState> _keys = new();
    readonly IEnumerable<Key> _all = Enum.GetValues<Key>();

    public Keyboard()
    {
        foreach (var key in _all)
        {
            if (key != Any && key != Unknown)
                _keys.Add(key, Up);
        }
    }

    internal void Press(Key key)
    {
        if (key == Unknown) return;
        _keys[key] = Pressed;
        OnKeyPress?.Invoke(key);
    }

    internal void Release(Key key)
    {
        if (key == Unknown) return;
        _keys[key] = Released;
        OnKeyRelease?.Invoke(key);
    }

    internal void Update()
    {
        _keys.Where((_, status) => status == (int) Pressed).ToList().ForEach(p =>
        {
            var key = p.Key;
            _keys[key] = Down;
        });
        _keys.Where((_, status) => status == (int) Released).ToList().ForEach(p =>
        {
            var key = p.Key;
            _keys[key] = Up;
        });
    }

    public bool IsUp(Key key)
    {
        if (key == Unknown) return false;
        if (key == Any)
        {
            var keys = _keys.Values;
            if (keys.Any(state => state is Up or Released))
                return true;
        }
        else
        {
            return _keys[key] == Up || WasReleased(key);
        }

        return false;
    }

    public bool IsDown(Key key)
    {
        if (key == Unknown) return false;
        if (key == Any)
        {
            var keys = _keys.Values;
            if (keys.Any(state => state is Down or Pressed))
                return true;
        }
        else
        {
            return _keys[key] == Down || WasPressed(key);
        }
        return false;
    }

    public bool WasReleased(Key key)
    {
        if (key == Unknown) return false;
        if (key == Any)
        {
            var keys = _keys.Values;
            if (keys.Any(state => state == Released))
                return true;
        }
        else
        {
            return _keys[key] == Released;
        }
        return false;
    }

    public bool WasPressed(Key key)
    {
        if (key == Unknown) return false;
        if (key == Any)
        {
            var keys = _keys.Values;
            if (keys.Any(state => state == Pressed))
                return true;
        }
        else
        {
            return _keys[key] == Pressed;
        }
        return false;
    }
}