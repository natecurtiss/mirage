namespace Mirage;

/// <summary>
/// An abstraction that represents an object with <see cref="Bounds"/>.
/// </summary>
public interface Boundable
{
    /// <summary>
    /// The <see cref="Bounds"/> of the <see cref="Boundable"/> object.
    /// </summary>
    Bounds Bounds { get; }
}