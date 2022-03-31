namespace Mirage.Rendering;

/// <summary>
/// <see cref="Exception"/> thrown when OpenGL fails to link a <see cref="Shader"/> program.
/// </summary>
sealed class ShaderProgramFailedLinkException : Exception
{
    public ShaderProgramFailedLinkException() { }
    public ShaderProgramFailedLinkException(string message) : base(message) { }
    public ShaderProgramFailedLinkException(string message, Exception inner) : base(message, inner) { }
}