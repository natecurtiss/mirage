using Silk.NET.Windowing;
using Native = Silk.NET.Windowing.Window;

namespace Guap;

public sealed class Window
{
    readonly WindowOptions _options;
    IWindow _native;

    public Window(string title, uint width, uint height, bool maximized = false, bool resizable = true)
    {
        _options = WindowOptions.Default;
        _options.Title = title;
        _options.Size = new((int) width, (int) height);
        _options.WindowBorder = resizable ? WindowBorder.Resizable : WindowBorder.Fixed;
        _options.WindowState = maximized ? WindowState.Maximized : WindowState.Normal;
    }

    public void Close() => _native?.Close();

    internal void Load(Action onOpen, Action onClose, Action<float> onUpdate, Action onRender)
    {
        _native = Native.Create(_options);
        _native.Load += () =>
        {
            _native.Center();
            onOpen();
        };
        _native.Closing += onClose;
        _native.Update += dt => onUpdate((float) dt);
        _native.Render += _ => onRender();
        _native.Run();
    }
}