using System;
using System.Numerics;
using Guap.Utilities;
using Silk.NET.OpenGL;
using static System.Numerics.Matrix4x4;

namespace Guap.Rendering;

sealed class Sprite : IDisposable
{
    public readonly Shader Shader;
    public readonly Texture Texture;
    readonly Transform _transform;

    public int SortingOrder { get; set; }
    
    public Sprite(Texture texture, GL gl, Transform transform)
    {
        Shader = new(gl, "Assets/sprite.vert".Find(), "Assets/sprite.frag".Find());
        Texture = texture;
        _transform = transform;
    }

    public Sprite(string path, GL gl, Transform transform) : this(new Texture(gl, path), gl, transform) { }
    
    public void Dispose()
    {
        Shader.Dispose();
        Texture.Dispose();
    }
    
    public Matrix4x4 ModelMatrix() => 
        CreateScale(_transform.Size.X * _transform.Scale.X, _transform.Size.Y * _transform.Scale.Y, 1f) * 
        CreateRotationZ(_transform.Rotation.ToRadians()) * 
        CreateTranslation(_transform.Position.X, _transform.Position.Y, 0f);
}