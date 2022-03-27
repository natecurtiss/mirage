using System;
using Silk.NET.OpenGL;

namespace Guap.Rendering;

public sealed class Graphics : IDisposable
{
    internal GL Lib { get; set; }
    public void Dispose() => Lib?.Dispose();
}