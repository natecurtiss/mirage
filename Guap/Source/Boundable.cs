using System.Numerics;

namespace Guap;

public interface Boundable
{
    Bounds Bounds();
    public Vector2 Center => Bounds().Center;
    public Vector2 Size => Bounds().Size;
    public Vector2 Extents => Bounds().Extents;
    public Vector2 Left => Bounds().Left;
    public Vector2 Right => Bounds().Right;
    public Vector2 Bottom => Bounds().Bottom;
    public Vector2 Top => Bounds().Top;
    public bool Overlaps(Boundable other) => Bounds().Overlaps(other.Bounds());
}