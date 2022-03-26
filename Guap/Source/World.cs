using Guap.Rendering;
using Guap.Input;
using Silk.NET.OpenGL;

namespace Guap;

public sealed class World : IDisposable
{
    readonly List<Entity> _entities = new();
    readonly GL _gl;
    readonly Renderer _renderer;
    readonly Camera _camera;
    readonly Window _window;
    readonly Keyboard _keyboard;

    public World(Window window, Keyboard keyboard, Graphics graphics, Renderer renderer, Camera camera)
    {
        _gl = graphics.Lib;
        _renderer = renderer;
        _camera = camera;
        _window = window;
        _keyboard = keyboard;
    }

    public void Dispose()
    {
        foreach (var entity in _entities.ToArray()) 
            entity.Dispose();
    }

    public World Spawn<T>() where T : Entity, new() => Spawn<T>(out _);

    public World Spawn<T>(out T entity) where T : Entity, new()
    {
        entity = new();
        entity.Initialize(_gl, this, _renderer, _camera, _window, _keyboard);
        _entities.Add(entity);
        return this;
    }

    public World Kill(Entity entity)
    {
        _entities.Remove(entity);
        entity.Destroy();
        return this;
    }

    internal void Start()
    {
        foreach (var entity in _entities) 
            entity.Start();
    }

    internal void Update(float dt)
    {
        foreach (var entity in _entities) 
            entity.Update(dt);
    }

    internal void Render()
    {
        foreach (var entity in _entities)
            entity.Render();
    }
}