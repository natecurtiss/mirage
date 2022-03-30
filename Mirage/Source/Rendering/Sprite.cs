using System;
using System.Numerics;
using Mirage.Utils;
using Silk.NET.OpenGL;
using static System.Numerics.Matrix4x4;

namespace Mirage.Rendering;

/// <summary>
/// An object with a <see cref="Texture"/> and <see cref="Shader"/> that can be rendered by a <see cref="Renderer"/>.
/// </summary>
sealed class Sprite : IDisposable
{
    /// <summary>
    /// The <see cref="Shader"/> used when rendering the <see cref="Sprite"/>.
    /// </summary>
    public readonly Shader Shader;
    
    /// <summary>
    /// The <see cref="Texture"/> used when rendering the <see cref="Sprite"/>.
    /// </summary>
    public readonly Texture Texture;
    
    readonly Transform _transform;

    /// <summary>
    /// The sorting order used to render the <see cref="Sprite"/>; higher value -> on top.
    /// </summary>
    public int SortingOrder { get; set; }
    
    /// <summary>
    /// Gets a <see cref="Matrix4x4"/> representing the transformations to apply the <see cref="Sprite"/> when rendered.
    /// </summary>
    public Matrix4x4 ModelMatrix =>
        CreateScale(_transform.Size.X * _transform.Scale.X, _transform.Size.Y * _transform.Scale.Y, 1f) *
        CreateRotationZ(_transform.Rotation.ToRadians()) *
        CreateTranslation(_transform.Position.X, _transform.Position.Y, 0f);

    /// <summary>
    /// Creates a new <see cref="Sprite"/>.
    /// </summary>
    /// <param name="path">The path to the image file used for the <see cref="Texture"/>.</param>
    /// <param name="gl">The OpenGL instance provided by the <see cref="Graphics"/> object.</param>
    /// <param name="transform">The <see cref="Transform"/> provided by the <see cref="Entity"/> using the <see cref="Sprite"/>.</param>
    public Sprite(string path, GL gl, Transform transform)
    {
        Shader = new(gl, "Assets/sprite.vert".Find(), "Assets/sprite.frag".Find());
        Texture = new(gl, path);
        _transform = transform;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Shader.Dispose();
        Texture.Dispose();
    }
}