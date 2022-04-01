namespace Mirage.Rendering;

/// <summary>
/// An <see cref="Entity{T}"/> that doesn't contain any logic - just a <see cref="Sprite"/> and <see cref="Transform"/> properties.
/// </summary>
public sealed class Prop : Entity<PropConfig>
{
    PropConfig _config;

    /// <inheritdoc />
    protected override void OnConfigure(PropConfig config) => _config = config;

    /// <inheritdoc />
    protected override void OnStart()
    {
        Texture = _config.Texture;
        SortingOrder = _config.SortingOrder;
        Position = _config.Position;
        Size = _config.Size;
        Scale = _config.Scale;
        Rotation = _config.Rotation;
    }
}