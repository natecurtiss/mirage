using System.Numerics;

namespace Guap;

public static class Math
{
    public static float ToRadians(this float degrees) => MathF.PI / 180f * degrees;
    public static float ToDegrees(this float radians) => 180f / MathF.PI * radians;

    public static Vector2 Normalized(this Vector2 v)
    {
        if (v.X == 0 && v.Y == 0)
            return v;
        return Vector2.Normalize(v);
    }
}