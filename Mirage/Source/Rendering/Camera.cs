using System.Numerics;

namespace Mirage.Rendering;

/// <summary>
/// A <see cref="Camera"/> that sees the <see cref="World"/>; can zoom in/out and move.
/// </summary>
/// <remarks>There should only ever be one instance of <see cref="Camera"/> created.</remarks>
public sealed class Camera
{
    readonly Window _window;

    /// <summary>
    /// The Position of the <see cref="Camera"/> in the <see cref="World"/>.
    /// </summary>
    public Vector2 Position { get; set; }
    
    /// <summary>
    /// The amount the <see cref="Camera"/> should zoom in; lower values -> larger objects.
    /// </summary>
    public float Zoom { get; set; } = 1f;
    
    /// <summary>
    /// Gets the <see cref="Camera"/>'s orthographic projection as a <see cref="Matrix4x4"/>.
    /// </summary>
    internal Matrix4x4 ProjectionMatrix
    {
        get
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

    /// <summary>
    /// Creates a new <see cref="Camera"/>.
    /// </summary>
    /// <param name="window">The <see cref="Window"/> used by the <see cref="Game"/>.</param>
    public Camera(Window window) => _window = window;
}