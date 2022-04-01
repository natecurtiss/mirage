namespace Samples.FlappyBird;

readonly struct BGConfig
{
    public readonly string Texture;
    public readonly int SortingOrder;
    public readonly Vector2 StartingPosition;
    public readonly Vector2 Size;
    public readonly float ScrollSpeed;

    public BGConfig(string texture, int sortingOrder, Vector2 startingPosition, Vector2 size, float scrollSpeed)
    {
        Texture = texture;
        SortingOrder = sortingOrder;
        StartingPosition = startingPosition;
        Size = size;
        ScrollSpeed = scrollSpeed;
    }

    public BGConfig At(Vector2 start) => new(Texture, SortingOrder, start, Size, ScrollSpeed);
}