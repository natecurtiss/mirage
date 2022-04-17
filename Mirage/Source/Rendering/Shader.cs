using System.IO;
using Silk.NET.OpenGL;

namespace Mirage.Rendering;

/// <summary>
/// A representation of a <see cref="Shader"/> used by OpenGL.
/// </summary>
sealed class Shader : IDisposable
{
    readonly GL _gl;
    readonly uint _shader;

    /// <summary>
    /// Creates a new <see cref="Shader"/>.
    /// </summary>
    /// <param name="gl">The OpenGL instance provided by the <see cref="Graphics"/> object.</param>
    /// <param name="vertexSource">The vertex shader source code.</param>
    /// <param name="fragmentSource">The fragment shader source code.</param>
    /// <exception cref="ShaderProgramFailedLinkException">Thrown if OpenGL fails to link the <see cref="Shader"/> program.</exception>
    public Shader(GL gl, string vertexSource, string fragmentSource)
    {
        _gl = gl;
        var vertex = Load(ShaderType.VertexShader, vertexSource);
        var fragment = Load(ShaderType.FragmentShader, fragmentSource);
        _shader = _gl.CreateProgram();
        _gl.AttachShader(_shader, vertex);
        _gl.AttachShader(_shader, fragment);
        _gl.LinkProgram(_shader);
        _gl.GetProgram(_shader, GLEnum.LinkStatus, out var status);
        if (status == 0)
            throw new ShaderProgramFailedLinkException($"Program failed to link with error: {_gl.GetProgramInfoLog(_shader)}");
        _gl.DetachShader(_shader, vertex);
        _gl.DetachShader(_shader, fragment);
        _gl.DeleteShader(vertex);
        _gl.DeleteShader(fragment);
    }

    /// <summary>
    /// Tells OpenGL to use this <see cref="Shader"/>.
    /// </summary>
    public void Use() => _gl.UseProgram(_shader);

    /// <summary>
    /// Sets a <see cref="Matrix4x4">mat4</see> uniform on the <see cref="Shader"/>.
    /// </summary>
    /// <param name="name">The name of the uniform.</param>
    /// <param name="value">The <see cref="Matrix4x4"/> value to set the uniform to.</param>
    /// <exception cref="MissingUniformOnShaderException">Thrown if the uniform does not exist on the <see cref="Shader"/>.</exception>
    /// <seealso cref="SetUniform(string,int)"/>
    /// <seealso cref="SetUniform(string,float)"/>
    /// <seealso cref="SetUniform(string,System.Numerics.Vector4)"/>
    public unsafe void SetUniform(string name, Matrix4x4 value)
    {
        var location = _gl.GetUniformLocation(_shader, name);
        if (location == -1)
            throw new MissingUniformOnShaderException($"{name} uniform not found on shader.");
        _gl.UniformMatrix4(location, 1, false, (float*) &value);
    }

    /// <summary>
    /// Sets an <see cref="int">int</see> uniform on the <see cref="Shader"/>.
    /// </summary>
    /// <param name="name">The name of the uniform.</param>
    /// <param name="value">The <see cref="int">int</see> value to set the uniform to.</param>
    /// <exception cref="MissingUniformOnShaderException">Thrown if the uniform does not exist on the <see cref="Shader"/>.</exception>
    /// <seealso cref="SetUniform(string,System.Numerics.Matrix4x4)"/>
    /// <seealso cref="SetUniform(string,float)"/>
    /// <seealso cref="SetUniform(string,System.Numerics.Vector4)"/>
    public void SetUniform(string name, int value)
    {
        var location = _gl.GetUniformLocation(_shader, name);
        if (location == -1)
            throw new MissingUniformOnShaderException($"{name} uniform not found on shader.");
        _gl.Uniform1(location, value);
    }

    /// <summary>
    /// Sets a <see cref="float">float</see> uniform on the <see cref="Shader"/>.
    /// </summary>
    /// <param name="name">The name of the uniform.</param>
    /// <param name="value">The <see cref="float">float</see> value to set the uniform to.</param>
    /// <exception cref="MissingUniformOnShaderException">Thrown if the uniform does not exist on the <see cref="Shader"/>.</exception>
    /// <seealso cref="SetUniform(string,System.Numerics.Matrix4x4)"/>
    /// <seealso cref="SetUniform(string,int)"/>
    /// <seealso cref="SetUniform(string,System.Numerics.Vector4)"/>
    public void SetUniform(string name, float value)
    {
        var location = _gl.GetUniformLocation(_shader, name);
        if (location == -1)
            throw new MissingUniformOnShaderException($"{name} uniform not found on shader.");
        _gl.Uniform1(location, value);
    }

    /// <summary>
    /// Sets a <see cref="Vector4">vec4</see> uniform on the <see cref="Shader"/>.
    /// </summary>
    /// <param name="name">The name of the uniform.</param>
    /// <param name="value">The <see cref="Vector4"/> value to set the uniform to.</param>
    /// <exception cref="MissingUniformOnShaderException">Thrown if the uniform does not exist on the <see cref="Shader"/>.</exception>
    /// <seealso cref="SetUniform(string,System.Numerics.Matrix4x4)"/>
    /// <seealso cref="SetUniform(string,float)"/>
    /// <seealso cref="SetUniform(string,int)"/>
    public void SetUniform(string name, Vector4 value)
    {
        var location = _gl.GetUniformLocation(_shader, name);
        if (location == -1)
            throw new MissingUniformOnShaderException($"{name} uniform not found on shader.");
        _gl.Uniform4(location, value.X, value.Y, value.Z, value.W);
    }

    uint Load(ShaderType type, string source)
    {
        var loaded = _gl.CreateShader(type);
        _gl.ShaderSource(loaded, source);
        _gl.CompileShader(loaded);
        var infoLog = _gl.GetShaderInfoLog(loaded);
        if (!string.IsNullOrWhiteSpace(infoLog))
            throw new ShaderFailedCompilationException($"Error compiling shader of type {type}, failed with error {infoLog}");
        return loaded;
    }
    
    /// <inheritdoc />
    public void Dispose() => _gl.DeleteProgram(_shader);
}