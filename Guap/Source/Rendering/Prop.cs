namespace Guap.Rendering;

public sealed class Prop : Entity<PropConfig>
{
    PropConfig _config;

    protected override void OnConfigure(PropConfig config) => _config = config;

    protected override void OnStart()
    {
        Texture = _config.Texture;
        Position = _config.Position;
        Size = _config.Scale;
        Rotation = _config.Rotation;
    }
}