using System.Numerics;
using Silk.NET.OpenGL;
using static System.Numerics.Matrix4x4;

namespace Guap.Rendering;

public sealed class Sprite : IDisposable
{
    internal readonly Shader Shader;
    internal readonly Texture Texture;
    readonly Entity _entity;

    internal Sprite(Entity entity, GL gl, string path)
    {
        _entity = entity;
        Shader = new(gl, "Assets/Shaders/sprite.vert".Find(), "Assets/Shaders/sprite.frag".Find());
        Texture = new(gl, path);
    }

    public void Dispose()
    {
        Shader.Dispose();
        Texture.Dispose();
    }
    
    public Matrix4x4 ModelMatrix() => 
        CreateScale(_entity.Scale.X, _entity.Scale.Y, 1f) * 
        CreateRotationZ(_entity.Rotation.ToRadians()) * 
        CreateTranslation(_entity.Position.X, _entity.Position.Y, 0f);
}