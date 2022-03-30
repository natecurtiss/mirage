using System.Numerics;

namespace Mirage.Rendering;

/// <summary>
/// The configuration for a <see cref="Prop"/>.
/// </summary>
public struct PropConfig
{
    /// <inheritdoc cref="Entity{T}.Texture"/>
    public readonly string Texture;
    /// <inheritdoc cref="Entity{T}.SortingOrder"/>
    public readonly int SortingOrder;
    /// <inheritdoc cref="Entity{T}.Position"/>
    public readonly Vector2 Position;
    /// <inheritdoc cref="Entity{T}.Scale"/>
    public readonly Vector2 Scale;
    /// <inheritdoc cref="Entity{T}.Rotation"/>
    public readonly float Rotation;

    /// <summary>
    /// Creates a new <see cref="PropConfig"/> with specified values.
    /// </summary>
    public PropConfig(string texture, int sortingOrder, Vector2 position, Vector2 scale, float rotation)
    {
        Texture = texture;
        SortingOrder = sortingOrder;
        Position = position;
        Scale = scale;
        Rotation = rotation;
    }

    /// <summary>
    /// Returns a copy of the <see cref="PropConfig"/> with a specified <see cref="Texture"/>.
    /// </summary>
    public PropConfig WithTexture(string texture) => this = new(texture, SortingOrder, Position, Scale, Rotation);
    
    /// <summary>
    /// Returns a copy of the <see cref="PropConfig"/> with a specified <see cref="SortingOrder"/>.
    /// </summary>
    public PropConfig WithSortingOrder(int sortingOrder) => this = new(Texture, sortingOrder, Position, Scale, Rotation);
    
    /// <summary>
    /// Returns a copy of the <see cref="PropConfig"/> with a specified <see cref="Position"/>.
    /// </summary>
    public PropConfig AtPosition(Vector2 position) => this = new(Texture, SortingOrder, position, Scale, Rotation);
    
    /// <summary>
    /// Returns a copy of the <see cref="PropConfig"/> with a specified <see cref="Scale"/>.
    /// </summary>
    public PropConfig WithScale(Vector2 scale) => this = new(Texture, SortingOrder, Position, scale, Rotation);
    
    /// <summary>
    /// Returns a copy of the <see cref="PropConfig"/> with a specified <see cref="Rotation"/>.
    /// </summary>
    public PropConfig WithRotation(float rotation) => this = new(Texture, SortingOrder, Position, Scale, rotation);
}