namespace Mirage.Rendering;

/// <summary>
/// <see cref="Exception"/> thrown when invalid <see cref="Shader"/> source is attempted to be compiled.
/// </summary>
sealed class ShaderFailedCompilationException : Exception
{
    public ShaderFailedCompilationException() { }
    public ShaderFailedCompilationException(string message) : base(message) { }
    public ShaderFailedCompilationException(string message, Exception inner) : base(message, inner) { }
}