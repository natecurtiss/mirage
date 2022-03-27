using Guap;

namespace Pong;

sealed class Ball : Entity<BallOptions>
{
    BallOptions _settings;
    
    protected override void OnConfigure(BallOptions settings) => _settings = settings;

    protected override void OnStart()
    {
        Texture = "Assets/square.png".Find();
        Scale = _settings.Scale;
    }
}