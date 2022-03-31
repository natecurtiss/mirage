namespace Mirage.Utils;

/// <summary>
/// Helper functions for generating random numbers.
/// </summary>
public static class Random
{
    static readonly int _magicToInt = 1000000000;
    static readonly System.Random _random = new();
    
    /// <summary>
    /// Returns a random number between the specified min and max.
    /// </summary>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    public static float Between(float min, float max)
    {
        var result = _random.Next((int) (min * _magicToInt), (int) (max * _magicToInt));
        return result / (float) _magicToInt;
    }
}