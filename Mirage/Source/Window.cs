using System;
using System.Drawing;
using System.Numerics;
using Mirage.Utils;
using Silk.NET.Core;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Key = Mirage.Input.Key;

namespace Mirage;

public sealed class Window : IDisposable, Boundable
{
    public readonly int Width;
    public readonly int Height;
    readonly WindowOptions _options;
    readonly Icon _default = new("Resources/logo.png".Find());
    
    IWindow _native;
    Icon _icon;

    public Color Background { get; set; } = Color.Black;
    public Icon Icon
    {
        get => _icon;
        set
        {
            var icon = value ?? _default;
            var raw = new RawImage(icon.Width, icon.Height, icon.Pixels);
            _native.SetWindowIcon(ref raw);
            _icon = icon;
        }
    }

    public Window(string title, uint width, uint height, bool maximized = false, bool resizable = true, string icon = null)
    {
        Width = (int) width;
        Height = (int) height;
        _options = WindowOptions.Default;
        _options.Title = title;
        _options.Size = new((int) width, (int) height);
        _options.WindowBorder = resizable ? WindowBorder.Resizable : WindowBorder.Fixed;
        _options.WindowState = maximized ? WindowState.Maximized : WindowState.Normal;
        _icon = icon is null ? _default : new(icon);
    }

    public void Dispose() => _native?.Dispose();

    public void Close() => _native?.Close();

    public Bounds Bounds() => new(Vector2.Zero, new(Width, Height));

    internal GL CreateGL() => _native?.CreateOpenGL();

    internal void Load(Action onOpen, Action onClose, Action<float> onUpdate, Action onRender, Action<Key> onKeyPress, Action<Key> onKeyRelease)
    {
        _native = Silk.NET.Windowing.Window.Create(_options);
        _native.Load += () =>
        {
            var input = _native.CreateInput().Keyboards[0];
            input.KeyDown += (_, key, _) => onKeyPress((Key) (int) key);
            input.KeyUp += (_, key, _) => onKeyRelease((Key) (int) key);
            _native.Center();
            Icon = _icon;
            onOpen();
        };
        _native.Closing += onClose;
        _native.Update += dt => onUpdate((float) dt);
        _native.Render += _ => onRender();
        _native.Run();
    }
}