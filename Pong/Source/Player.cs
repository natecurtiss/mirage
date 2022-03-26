using Guap;

namespace Pong;

sealed class Player : Entity<PlayerOptions>
{
    protected override void OnConfigure(PlayerOptions settings) => Texture = settings.Texture;

    protected override void OnUpdate(float dt)
    {
        
    }
}