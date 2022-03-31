using Silk.NET.OpenGL;

namespace Mirage.Rendering;

/// <summary>
/// A representation of a VAO in OpenGL.
/// </summary>
/// <typeparam name="TVertex">The data type used by the <see cref="BufferObject{T}">VBO.</see></typeparam>
/// <typeparam name="TIndex">The data type used by the <see cref="BufferObject{T}">EBO.</see></typeparam>
sealed class VertexArrayObject<TVertex, TIndex> : IDisposable 
    where TVertex : unmanaged 
    where TIndex : unmanaged
{
    readonly uint _handle;
    readonly GL _gl;

    /// <summary>
    /// Creates a <see cref="VertexArrayObject{TVertex,TIndex}"/>.
    /// </summary>
    /// <param name="gl">The OpenGL instance provided by the <see cref="Graphics"/> object.</param>
    /// <param name="vbo">The <see cref="BufferObject{T}">VBO</see> that contains the vertex data.</param>
    /// <param name="ebo">The <see cref="BufferObject{T}">EBO</see> that contains the index data.</param>
    public VertexArrayObject(GL gl, BufferObject<TVertex> vbo, BufferObject<TIndex> ebo)
    {
        _gl = gl;
        _handle = _gl.GenVertexArray();
        Bind();
        vbo.Bind();
        ebo.Bind();
    }
    
    /// <summary>
    /// Tells OpenGL how to use the data given in the <see cref="BufferObject{T}">VBO</see>.
    /// </summary>
    /// <param name="index">The index of the vertex attribute to set.</param>
    /// <param name="count">The number of values to use per vertex.</param>
    /// <param name="type">The type of data used in the vertex attribute array.</param>
    /// <param name="vertexSize">The size of the vertex attribute.</param>
    /// <param name="offset">The offset of the data in the array per vertex.</param>
    public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offset)
    {
        _gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint) sizeof(TVertex), (void*) (offset * sizeof(TVertex)));
        _gl.EnableVertexAttribArray(index);
    }

    /// <summary>
    /// Binds the <see cref="VertexArrayObject{TVertex,TIndex}">VAO.</see>
    /// </summary>
    public void Bind() => _gl.BindVertexArray(_handle);
    
    /// <inheritdoc />
    public void Dispose() => _gl.DeleteVertexArray(_handle);
}