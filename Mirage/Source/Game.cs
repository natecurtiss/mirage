using Mirage.Graphics;
using Mirage.Input;
using Silk.NET.OpenGL;

namespace Mirage;

public sealed class Game
{
    readonly World _world;
    readonly Window _window;
    readonly Keyboard _keyboard;
    readonly Graphics.Graphics _graphics;
    readonly Renderer _renderer;

    public GL GL { get; private set; }

    public Game(World world, Window window, Keyboard keyboard, Graphics.Graphics graphics, Renderer renderer)
    {
        _world = world;
        _window = window;
        _keyboard = keyboard;
        _graphics = graphics;
        _renderer = renderer;
    }
    
    public void Dispose()
    {
        _world.Dispose();
        _renderer.Dispose();
        _window.Dispose();
        _graphics.Dispose();
    }

    public void Start() => _window.Load(() =>
    {
        _graphics.Lib = _window.CreateGraphicsLibrary();
        _renderer.Initialize(_graphics.Lib);
        _world.Start();
    }, Dispose, Update, Render, _keyboard.Press, _keyboard.Release);

    void Update(float dt)
    {
        _keyboard.Update();
        _world.Update(dt);
    }

    void Render()
    {
        _world.Render();
        _renderer.Display();
    }
}