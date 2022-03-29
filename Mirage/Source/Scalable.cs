using System.Numerics;

namespace Mirage;

public interface Scalable
{
    public Vector2 Size { get; set; }
    public Vector2 Scale { get; set; }
}