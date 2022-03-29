using System.Numerics;

namespace Mirage.Graphics;

public struct PropConfig
{
    public readonly string Texture;
    public readonly int SortingOrder;
    public readonly Vector2 Position;
    public readonly Vector2 Scale;
    public readonly float Rotation;

    public PropConfig(string texture, int sortingOrder, Vector2 position, Vector2 scale, float rotation)
    {
        Texture = texture;
        SortingOrder = sortingOrder;
        Position = position;
        Scale = scale;
        Rotation = rotation;
    }

    public PropConfig WithTexture(string texture) => this = new(texture, SortingOrder, Position, Scale, Rotation);
    public PropConfig WithSortingOrder(int sortingOrder) => this = new(Texture, sortingOrder, Position, Scale, Rotation);
    public PropConfig AtPosition(Vector2 position) => this = new(Texture, SortingOrder, position, Scale, Rotation);
    public PropConfig WithScale(Vector2 scale) => this = new(Texture, SortingOrder, Position, scale, Rotation);
    public PropConfig WithRotation(float rotation) => this = new(Texture, SortingOrder, Position, Scale, rotation);
}