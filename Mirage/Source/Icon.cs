using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Silk.NET.Core;

namespace Mirage;

/// <summary>
/// A representation of an icon for the <see cref="Window"/>.
/// </summary>
public sealed class Icon
{
    /// <summary>
    /// The width in pixels of the <see cref="Icon"/>.
    /// </summary>
    internal readonly int Width;
    
    /// <summary>
    /// The height in pixels of the <see cref="Icon"/>.
    /// </summary>
    internal readonly int Height;

    /// <summary>
    /// The <see cref="Icon"/> as a <see cref="RawImage"/>.
    /// </summary>
    internal readonly RawImage Raw;

    /// <summary>
    /// Creates a new <see cref="Icon"/>.
    /// </summary>
    /// <param name="path">The path to the <see cref="Icon"/>'s image file.</param>
    public Icon(string path)
    {
        using var image = new Bitmap(path);
        using var resized = (Bitmap) image.GetThumbnailImage(256, 256, () => false, IntPtr.Zero);
        for (var y = 0; y < resized.Height; y++)
        {
            for (var x = 0; x < resized.Width; x++)
            {
                var pixel = resized.GetPixel(x, y);
                var a = pixel.A;
                var r = pixel.R;
                var g = pixel.G;
                var b = pixel.B;
                resized.SetPixel(x, y, Color.FromArgb(a, b, g, r));
            }
        }
        var rect = new Rectangle(0, 0, resized.Width, resized.Height);
        var data = resized.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        var length = data.Stride * data.Height;
        var bytes = new byte[length];
        Marshal.Copy(data.Scan0, bytes, 0, length);
        resized.UnlockBits(data);
        
        Width = resized.Width;
        Height = resized.Height;
        Raw = new(Width, Height, bytes);
    }
}