using System.Collections.Generic;
using System.Linq;
using Silk.NET.OpenGL;

namespace Mirage.Rendering;

/// <summary>
/// The module responsible for rendering <see cref="Sprite"/>s to the screen.
/// </summary>
/// <remarks>There should only be one instance of a <see cref="Renderer"/> created.</remarks>
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

    /// <summary>
    /// Creates a new <see cref="Renderer"/>.
    /// </summary>
    /// <param name="camera">The <see cref="Camera"/> used by the <see cref="Game"/>.</param>
    /// <param name="window">The <see cref="Window"/> used by the <see cref="Game"/>.</param>
    public Renderer(Camera camera, Window window)
    {
        _camera = camera;
        _window = window;
    }

    /// <summary>
    /// Initializes the <see cref="Renderer"/>'s VAO with a VBO and EBO.
    /// </summary>
    /// <param name="gl">The GL object obtained from <see cref="Graphics.Lib">Graphics.Lib</see>.</param>
    internal void Initialize(GL gl)
    {
        _gl = gl;
        _vbo = new(_gl, _vertices, BufferTargetARB.ArrayBuffer);
        _ebo = new(_gl, _indices, BufferTargetARB.ElementArrayBuffer);
        _vao = new(_gl, _vbo, _ebo);

        _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
        _vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);
    }

    /// <summary>
    /// Queues a <see cref="Sprite"/> to be rendered on the current frame's render tick.
    /// </summary>
    /// <param name="sprite">The <see cref="Sprite"/> to be rendered.</param>
    internal void Queue(Sprite sprite) => _sprites.Add(sprite);

    /// <summary>
    /// Displays all the <see cref="Sprite"/>s in the render queue to the screen.
    /// </summary>
    internal unsafe void Display()
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        _gl.ClearColor(_window.Background);
        _vao.Bind();
        
        foreach (var sprite in _sprites.OrderBy(s => s.SortingOrder).Reverse())
        {
            sprite.Shader.Use();
            sprite.Texture.Bind();
            sprite.Shader.SetUniform("uTexture0", 0);
            sprite.Shader.SetUniform("uModel", sprite.ModelMatrix);
            sprite.Shader.SetUniform("uProjection", _camera.ProjectionMatrix);
            _gl.DrawElements(PrimitiveType.Triangles, (uint) _indices.Length, DrawElementsType.UnsignedInt, null);
        }
        
        _sprites.Clear();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _vbo.Dispose();
        _ebo.Dispose();
        _vao.Dispose();
        _sprites.Clear();
    }
}