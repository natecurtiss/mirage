using Guap.Utilities;

namespace Guap.Rendering;

public sealed class SpriteDebugger : Entity
{
    protected override void OnStart() => Texture = "Assets/Textures/n8dev.png".Find();
}