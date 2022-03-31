namespace Mirage;

/// <summary>
/// An abstraction that represents an object with a <see cref="Size"/> and <see cref="Scale"/>.
/// </summary>
public interface Scalable
{
    /// <summary>
    /// The size of the object.
    /// </summary>
    Vector2 Size { get; set; }
    
    /// <summary>
    /// The scale of the object.
    /// </summary>
    Vector2 Scale { get; set; }
}