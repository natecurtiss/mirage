using Silk.NET.OpenGL;

namespace Mirage.Rendering;

/// <summary>
/// A wrapper for OpenGL used by the <see cref="Game"/> and other modules.
/// </summary>
/// <remarks>There should only ever be one <see cref="Graphics"/> object created.</remarks>
public sealed class Graphics : IDisposable
{
    /// <summary>
    /// The OpenGL instance, set by the <see cref="Game"/>.
    /// </summary>
    /// <remarks>Can only be accessed after <see cref="Window.Load">Window.Load()</see> is called.</remarks>
    internal GL Lib { get; set; }
    
    /// <inheritdoc />
    public void Dispose() => Lib?.Dispose();
}