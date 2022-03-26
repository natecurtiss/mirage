using System.Drawing;
using System.Numerics;
using Silk.NET.OpenGL;
using static System.Numerics.Matrix4x4;

namespace Guap.Rendering;

sealed class Sprite : IDisposable
{
    internal readonly Shader Shader;
    internal readonly Texture Texture;
    readonly Entity _entity;

    internal Sprite(GL gl, string path, Entity entity)
    {
        Shader = new(gl, "Assets/Shaders/sprite.vert".Find(), "Assets/Shaders/sprite.frag".Find());
        Texture = new(gl, path);
        _entity = entity;
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