namespace Mirage.Utils;

/// <summary>
/// Mathematical helper functions.
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// Returns the degrees passed in as radians.
    /// </summary>
    public static float ToRadians(this float degrees) => MathF.PI / 180f * degrees;
    
    /// <summary>
    /// Returns the radians passed in as degrees.
    /// </summary>
    public static float ToDegrees(this float radians) => 180f / MathF.PI * radians;

    /// <summary>
    /// Returns the <see cref="Vector2"/> passed in normalized to a magnitude of 1.
    /// </summary>
    public static Vector2 Normalized(this Vector2 v)
    {
        if (v.X == 0 && v.Y == 0)
            return v;
        return Vector2.Normalize(v);
    }
}