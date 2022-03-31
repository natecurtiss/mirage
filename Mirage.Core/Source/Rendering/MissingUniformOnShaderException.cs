namespace Mirage.Rendering;

/// <summary>
/// <see cref="Exception"/> thrown when the user tries to set a non-existent uniform on a <see cref="Shader"/> with any of the <see cref="Shader">Shader.SetUniform()</see> overloads.
/// </summary>
sealed class MissingUniformOnShaderException : Exception
{
    /// <inheritdoc />
    public MissingUniformOnShaderException() { }
    
    /// <inheritdoc />
    public MissingUniformOnShaderException(string message) : base(message) { }
    
    /// <inheritdoc />
    public MissingUniformOnShaderException(string message, Exception inner) : base(message, inner) { }
}