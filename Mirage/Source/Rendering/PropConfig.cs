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
    /// <inheritdoc cref="Entity{T}.Size"/>
    public readonly Vector2 Size;
    /// <inheritdoc cref="Entity{T}.Scale"/>
    public readonly Vector2 Scale;
    /// <inheritdoc cref="Entity{T}.Rotation"/>
    public readonly float Rotation;

    /// <summary>
    /// Creates a new <see cref="PropConfig"/> with specified values.
    /// </summary>
    public PropConfig(string texture, Vector2 size, Vector2 scale, int sortingOrder = 0, Vector2 position = default, float rotation = 0f)
    {
        Texture = texture;
        SortingOrder = sortingOrder;
        Position = position;
        Size = size;
        Scale = scale;
        Rotation = rotation;
    }
}