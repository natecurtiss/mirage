using System;
using Silk.NET.OpenGL;

namespace Mirage.Graphics;

sealed class VertexArrayObject<TVertex, TIndex> : IDisposable 
    where TVertex : unmanaged 
    where TIndex : unmanaged
{
    readonly uint _handle;
    readonly GL _gl;

    public VertexArrayObject(GL gl, BufferObject<TVertex> vbo, BufferObject<TIndex> ebo)
    {
        _gl = gl;
        _handle = _gl.GenVertexArray();
        Bind();
        vbo.Bind();
        ebo.Bind();
    }
    
    public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offset)
    {
        _gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint) sizeof(TVertex), (void*) (offset * sizeof(TVertex)));
        _gl.EnableVertexAttribArray(index);
    }

    public void Bind() => _gl.BindVertexArray(_handle);
    public void Dispose() => _gl.DeleteVertexArray(_handle);
}