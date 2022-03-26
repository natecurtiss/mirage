using Silk.NET.OpenGL;

namespace Guap.Rendering;

sealed class Texture : IDisposable
{
    public readonly string Path;
    readonly GL _gl;
    
    public Texture(GL gl, string path)
    {
        _gl = gl;
        Path = path;
    }

    public void Bind()
    {
        
    }

    public void Dispose()
    {
        
    }
}