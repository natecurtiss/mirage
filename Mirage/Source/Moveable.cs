namespace Mirage;

/// <summary>
/// An abstraction that represents an object with a <see cref="Position"/>.
/// </summary>
public interface Moveable
{
    /// <summary>
    /// The position of the object.
    /// </summary>
    Vector2 Position { get; set; }
}