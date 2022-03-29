using System;
using System.Collections.Generic;
using Mirage.Graphics;
using Mirage.Input;

namespace Mirage;

public sealed class World : IDisposable
{
    readonly List<Entity> _starting = new();
    readonly List<Entity> _entities = new();
    readonly Graphics.Graphics _graphics;
    readonly Renderer _renderer;
    readonly Camera _camera;
    readonly Window _window;
    readonly Keyboard _keyboard;

    bool _hasStarted;
    Action _onInitialize;
    Action _onStart;

    public World(Window window, Keyboard keyboard, Graphics.Graphics graphics, Renderer renderer, Camera camera)
    {
        _graphics = graphics;
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

    public World Spawn<TE, TS>(TS settings) where TE : Entity<TS>, new() => Spawn<TE, TS>(out _, settings);
    public World Spawn<TE, TS>(TS settings, out TE entity) where TE : Entity<TS>, new() => Spawn<TE, TS>(out entity, settings);
    public World Spawn<TE, TS>(out TE entity, TS settings) where TE : Entity<TS>, new()
    {
        entity = new();
        entity.Configure(settings);
        if (!_hasStarted)
            _starting.Add(entity);
        else
            Create(entity);
        return this;
    }
    
    public World Spawn<T>() where T : Entity, new() => Spawn<T>(out _);
    public World Spawn<T>(out T entity) where T : Entity, new()
    {
        entity = new();
        if (!_hasStarted)
        {
            _starting.Add(entity);
        }
        else
        {
            entity.Initialize(_graphics.Lib, this, _renderer, _camera, _window, _keyboard);
            _entities.Add(entity);
            entity.Awake();
            entity.Start();
        }
        return this;
    }

    public World Kill(Entity entity)
    {
        _entities.Remove(entity);
        entity.Destroy();
        return this;
    }
    
    public World OnAwake(Action callback)
    {
        _onInitialize += callback;
        return this;
    }

    public World OnStart(Action callback)
    {
        _onStart += callback;
        return this;
    }

    internal void Start()
    {
        _hasStarted = true;
        _onInitialize?.Invoke();
        foreach (var entity in _starting)
        {
            entity.Initialize(_graphics.Lib, this, _renderer, _camera, _window, _keyboard);
            _entities.Add(entity);
        }
        foreach (var entity in _entities) 
            entity.Awake();
        foreach (var entity in _entities) 
            entity.Start();
        _starting.Clear();
        _onStart?.Invoke();
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

    void Create(Entity entity)
    {
        entity.Initialize(_graphics.Lib, this, _renderer, _camera, _window, _keyboard);
        _entities.Add(entity);
        entity.Start();
    }
}