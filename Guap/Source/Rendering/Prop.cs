namespace Guap.Rendering;

public sealed class Prop : Entity<PropOptions>
{
    PropOptions _options;

    protected override void OnConfigure(PropOptions config) => _options = config;

    protected override void OnStart()
    {
        Texture = _options.Texture;
        Position = _options.Position;
        Scale = _options.Scale;
        Rotation = _options.Rotation;
    }
}