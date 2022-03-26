using Guap.Input;

namespace Guap;

public sealed class World
{
    readonly List<Entity> _entities = new();
    readonly Keyboard _keyboard;

    public World(Keyboard keyboard) => _keyboard = keyboard;

    public World Spawn<T>() where T : Entity, new() => Spawn<T>(out _);

    public World Spawn<T>(out T entity) where T : Entity, new()
    {
        entity = new();
        entity.Initialize(_keyboard);
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