using System;
using Silk.NET.OpenGL;

namespace Mirage.Rendering;

/// <summary>
/// A representation of a Buffer Object in OpenGL, e.g, a VBO or an EBO.
/// </summary>
/// <typeparam name="T">The type of data to buffer.</typeparam>
sealed class BufferObject<T> : IDisposable where T : unmanaged
{
    readonly GL _gl;
    readonly uint _handle;
    readonly BufferTargetARB _bufferType;

    /// <summary>
    /// Creates a new <see cref="BufferObject{T}"/>.
    /// </summary>
    /// <param name="gl">The OpenGL instance provided by the <see cref="Graphics"/> object.</param>
    /// <param name="data">The data to initialize with, e.g, an array of vertices or indices.</param>
    /// <param name="bufferType">The type of buffer to use.</param>
    public unsafe BufferObject(GL gl, Span<T> data, BufferTargetARB bufferType)
    {
        _gl = gl;
        _bufferType = bufferType;
        _handle = _gl.GenBuffer();
        Bind();
        fixed (void* d = data)
        {
            _gl.BufferData(bufferType, (nuint) (data.Length * sizeof(T)), d, BufferUsageARB.StaticDraw);
        }
    }

    /// <summary>
    /// Binds the buffer.
    /// </summary>
    public void Bind() => _gl.BindBuffer(_bufferType, _handle);
    
    /// <inheritdoc />
    public void Dispose() => _gl.DeleteBuffer(_handle);
}