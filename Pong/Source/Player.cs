using Guap;
using Guap.Input;

namespace Pong;

sealed class Player : Entity<PlayerOptions>
{
    PlayerOptions _options;

    protected override void OnConfigure(PlayerOptions settings) => _options = settings;

    protected override void OnStart() => Texture = _options.Texture;

    protected override void OnUpdate(float dt)
    {
        
    }
}