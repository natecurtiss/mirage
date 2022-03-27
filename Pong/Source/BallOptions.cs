using System.Numerics;

namespace Pong;

readonly struct BallOptions
{
    public readonly Vector2 Scale;
    public readonly float Speed;
    public readonly float Multiplier;
    public readonly float MinBounce;
    public readonly float MaxBounce;

    public BallOptions(float scale, float speed, float multiplier, float minBounce, float maxBounce)
    {
        Scale = new(scale);
        Speed = speed;
        Multiplier = multiplier;
        MinBounce = minBounce;
        MaxBounce = maxBounce;
    }
}