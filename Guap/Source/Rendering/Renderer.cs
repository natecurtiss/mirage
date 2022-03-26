using Silk.NET.OpenGL;

namespace Guap.Rendering;

public sealed class Renderer : IDisposable
{
    readonly List<Sprite> _sprites = new();
    readonly float[] _vertices =
    { 
        //X     Y     Z     U   V
         0.5f,  0.5f, 0.0f, 1f, 1f,
         0.5f, -0.5f, 0.0f, 1f, 0f,
        -0.5f, -0.5f, 0.0f, 0f, 0f,
        -0.5f,  0.5f, 0.0f, 0f, 1f
    };
    readonly uint[] _indices =
    {
        0, 1, 3,
        1, 2, 3
    };
    readonly Camera _camera;
    readonly Window _window;
    
    BufferObject<float> _vbo;
    BufferObject<uint> _ebo;
    VertexArrayObject<float, uint> _vao;
    GL _gl;

    public Renderer(Camera camera, Window window)
    {
        _camera = camera;
        _window = window;
    }

    public void Initialize(GL gl)
    {
        _gl = gl;
        _vbo = new(_gl, _vertices, BufferTargetARB.ArrayBuffer);
        _ebo = new(_gl, _indices, BufferTargetARB.ElementArrayBuffer);
        _vao = new(_gl, _vbo, _ebo);

        _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
        _vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);
    }

    public void Queue(Sprite sprite) => _sprites.Add(sprite);

    public unsafe void Display()
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        _gl.ClearColor(_window.Background);
        _vao.Bind();

        foreach (var sprite in _sprites)
        {
            sprite.Shader.Use();
            sprite.Texture.Bind();
            sprite.Shader.SetUniform("uTexture0", 0);
            sprite.Shader.SetUniform("uModel", sprite.ModelMatrix());
            sprite.Shader.SetUniform("uProjection", _camera.ProjectionMatrix());
            _gl.DrawElements(PrimitiveType.Triangles, (uint) _indices.Length, DrawElementsType.UnsignedInt, null);
        }
        
        _sprites.Clear();
    }

    public void Dispose()
    {
        _vbo.Dispose();
        _ebo.Dispose();
        _vao.Dispose();
        _sprites.Clear();
    }
}