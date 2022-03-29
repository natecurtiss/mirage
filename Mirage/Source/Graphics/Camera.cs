using System.Numerics;

namespace Mirage.Graphics;

public sealed class Camera
{
    readonly Window _window;

    public Vector2 Position { get; set; }
    public float Zoom { get; set; } = 1f;

    public Camera(Window window) => _window = window;

    internal Matrix4x4 ProjectionMatrix()
    {
        var left = Position.X - _window.Width / 2f;
        var right = Position.X + _window.Width / 2f;
        var top = Position.Y + _window.Height / 2f;
        var bottom = Position.Y - _window.Height / 2f;

        var orthographicMatrix = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, 0.01f, 100f);
        var zoomMatrix = Matrix4x4.CreateScale(Zoom);
        return orthographicMatrix * zoomMatrix;
    }
}