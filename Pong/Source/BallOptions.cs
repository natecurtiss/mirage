using System.Numerics;

namespace Pong;

readonly struct BallOptions
{
    public readonly Vector2 Scale;

    public BallOptions(float scale)
    {
        Scale = new(scale);
    }
}