namespace Guap.Utilities;

public static class Random
{
    static readonly int _magicToInt = 1000000;
    static readonly System.Random _random = new();
    public static float Between(float min, float max)
    {
        var result = _random.Next((int) (min * _magicToInt), (int) (max * _magicToInt));
        return result / (float) _magicToInt;
    }
}