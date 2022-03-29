using Mirage.Utils;

namespace Mirage.Graphics;

public sealed class SpriteDebugger : Entity
{
    protected override void OnStart() => Texture = "Assets/Textures/n8dev.png".Find();
}