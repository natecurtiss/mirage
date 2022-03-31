using System.Collections.Generic;
using System.Linq;
using static Mirage.Input.Key;
using static Mirage.Input.KeyState;

namespace Mirage.Input;

/// <summary>
/// A representation of a real-life keyboard; the input system for the <see cref="Game"/>.
/// </summary>
/// <remarks>There should only ever be one instance of a <see cref="Keyboard"/> created.</remarks>
public sealed class Keyboard
{
    /// <summary>
    /// Event invoked when a <see cref="Key"/> is pressed after not being pressed during the previous frame.
    /// </summary>
    public event Action<Key> OnKeyPress;
    
    /// <summary>
    /// Event invoked when a <see cref="Key"/> is released after being held down during the previous frame.
    /// </summary>
    public event Action<Key> OnKeyRelease;
    
    readonly Dictionary<Key, KeyState> _keys = new();
    readonly IEnumerable<Key> _all = Enum.GetValues<Key>();

    /// <summary>
    /// Creates a <see cref="Keyboard"/>.
    /// </summary>
    public Keyboard()
    {
        foreach (var key in _all)
        {
            if (key != Any && key != Unknown)
                _keys.Add(key, Up);
        }
    }
    
    /// <summary>
    /// Returns true if the <see cref="Key"/> is currently not being pressed.
    /// </summary>
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

    /// <summary>
    /// Returns true if the <see cref="Key"/> is currently being pressed.
    /// </summary>
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

    /// <summary>
    /// Returns true if the <see cref="Key"/> was just released after being held down.
    /// </summary>
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

    /// <summary>
    /// Returns true if the <see cref="Key"/> was just pressed after being up.
    /// </summary>
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

    /// <summary>
    /// Tells the input system that a <see cref="Key"/> was just pressed.
    /// </summary>
    /// <param name="key">The <see cref="Key"/> that was just pressed.</param>
    internal void Press(Key key)
    {
        if (key == Unknown) return;
        _keys[key] = Pressed;
        OnKeyPress?.Invoke(key);
    }

    /// <summary>
    /// Tells the input system that a <see cref="Key"/> was just released.
    /// </summary>
    /// <param name="key">The <see cref="Key"/> that was just released..</param>
    internal void Release(Key key)
    {
        if (key == Unknown) return;
        _keys[key] = Released;
        OnKeyRelease?.Invoke(key);
    }

    /// <summary>
    /// Updates the state of every key; called every frame by the <see cref="Game"/>.
    /// </summary>
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
}