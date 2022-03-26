using Guap.Input;

namespace Guap;

public sealed class Game : IDisposable
{
    readonly Window _window;
    readonly Keyboard _keyboard;
    readonly World _world;

    public Game(Window window, Keyboard keyboard, World world)
    {
        _window = window;
        _keyboard = keyboard;
        _world = world;
    }
    
    public void Dispose()
    {
        
    }

    public void Start() => _window.Load(_world.Start, Dispose, Update, Render);

    void Update(float dt)
    {
        _keyboard.Update();
        _world.Update(dt);
    }

    void Render()
    {
        _world.Render();
    }
}