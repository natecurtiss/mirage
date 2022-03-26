using System.Numerics;
using Guap;

namespace Pong;

abstract class Paddle : Entity<PaddleOptions>
{
    protected PaddleOptions Options { get; private set; }

    protected sealed override void OnConfigure(PaddleOptions settings) => Options = settings;

    protected sealed override void OnStart()
    {
        Texture = "Assets/square.png".Find();
        Scale = new(10f, 100f);
        Position = Options.StartingPosition;
    }

    protected void ClampPosition() => Position = new(Position.X, Math.Clamp(Position.Y, Window.Bounds().Bottom.Y + Bounds().Extents.Y, Window.Bounds().Top.Y - Bounds().Extents.Y));
}