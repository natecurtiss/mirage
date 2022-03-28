using System;
using System.Drawing;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Guap;

public sealed class Window : IDisposable, Boundable
{
    public readonly int Width;
    public readonly int Height;
    readonly WindowOptions _options;
    
    IWindow _native;

    public Color Background { get; set; } = Color.Black;

    public Window(string title, uint width, uint height, bool maximized = false, bool resizable = true)
    {
        Width = (int) width;
        Height = (int) height;
        _options = WindowOptions.Default;
        _options.Title = title;
        _options.Size = new((int) width, (int) height);
        _options.WindowBorder = resizable ? WindowBorder.Resizable : WindowBorder.Fixed;
        _options.WindowState = maximized ? WindowState.Maximized : WindowState.Normal;
    }

    public void Dispose() => _native?.Dispose();

    public void Close() => _native?.Close();

    public Bounds Bounds() => new(Vector2.Zero, new(Width, Height));

    internal GL CreateGraphicsLibrary() => _native?.CreateOpenGL();

    internal void Load(Action onOpen, Action onClose, Action<float> onUpdate, Action onRender, Action<Guap.Input.Key> onKeyPress, Action<Guap.Input.Key> onKeyRelease)
    {
        _native = Silk.NET.Windowing.Window.Create(_options);
        _native.Load += () =>
        {
            var input = _native.CreateInput().Keyboards[0];
            input.KeyDown += (_, key, _) => onKeyPress((Guap.Input.Key) (int) key);
            input.KeyUp += (_, key, _) => onKeyRelease((Guap.Input.Key) (int) key);
            _native.Center();
            onOpen();
        };
        _native.Closing += onClose;
        _native.Update += dt => onUpdate((float) dt);
        _native.Render += _ => onRender();
        _native.Run();
    }
}