using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Silk.NET.OpenGL;
using PixelFormat = Silk.NET.OpenGL.PixelFormat;

namespace Mirage.Rendering;

/// <summary>
/// A representation of a <see cref="Texture"/> used by OpenGL.
/// </summary>
sealed class Texture : IDisposable
{
    /// <summary>
    /// The path to the image file used by the <see cref="Texture"/>.
    /// </summary>
    public readonly string Path;
    
    readonly GL _gl;
    readonly uint _handle;

    /// <summary>
    /// Creates a <see cref="Texture"/>.
    /// </summary>
    /// <param name="gl">The OpenGL instance provided by the <see cref="Graphics"/> object.</param>
    /// <param name="path">The path to the image file to be used by the <see cref="Texture"/>.</param>
    public unsafe Texture(GL gl, string path)
    {
        Path = path;
        _gl = gl;

        using var image = new Bitmap(path);
        image.RotateFlip(RotateFlipType.RotateNoneFlipY);

        _handle = _gl.GenTexture();
        Bind();
        
        var rect = new Rectangle(0, 0, image.Width, image.Height);
        var data = image.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        var length = data.Stride * data.Height;
        var bytes = new byte[length];
        
        Marshal.Copy(data.Scan0, bytes, 0, length);
        image.UnlockBits(data);

        fixed (void* pixels = &bytes[0])
        {
            _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint) image.Width, (uint) image.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, pixels);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) GLEnum.Repeat);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) GLEnum.Repeat);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) GLEnum.Linear);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) GLEnum.Linear);
            _gl.GenerateMipmap(TextureTarget.Texture2D);
        }
    }

    /// <summary>
    /// Binds the <see cref="Texture"/> to the specified active slot in OpenGL.
    /// </summary>
    /// <param name="textureSlot">The slot to bind to.</param>
    public void Bind(TextureUnit textureSlot = TextureUnit.Texture0)
    {
        _gl.ActiveTexture(textureSlot);
        _gl.BindTexture(TextureTarget.Texture2D, _handle);
    }

    /// <inheritdoc />
    public void Dispose() => _gl.DeleteTexture(_handle);
}