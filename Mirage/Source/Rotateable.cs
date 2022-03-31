namespace Mirage;

/// <summary>
/// An abstraction that represents an object with a <see cref="Rotation"/>.
/// </summary>
public interface Rotateable
{
    /// <summary>
    /// The rotation of the object.
    /// </summary>
    float Rotation { get; set; }
}