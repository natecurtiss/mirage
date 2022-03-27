using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Silk.NET.OpenGL;
using PixelFormat = Silk.NET.OpenGL.PixelFormat;

namespace Guap.Rendering;

sealed class Texture : IDisposable
{
    public readonly string Path;
    readonly GL _gl;
    readonly uint _handle;

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

    public void Bind(TextureUnit textureSlot = TextureUnit.Texture0)
    {
        _gl.ActiveTexture(textureSlot);
        _gl.BindTexture(TextureTarget.Texture2D, _handle);
    }

    public void Dispose() => _gl.DeleteTexture(_handle);
}