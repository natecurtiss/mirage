namespace Guap;

public sealed class World
{
    readonly List<Entity> _entities = new();

    public World Spawn<T>(out T entity) where T : Entity, new()
    {
        entity = Spawn<T>();
        return this;
    }

    public T Spawn<T>() where T : Entity, new()
    {
        var e = new T();
        _entities.Add(e);
        return e;
    }

    public World Kill(Entity entity)
    {
        _entities.Remove(entity);
        entity.Destroy();
        return this;
    }

    internal void Update(float dt)
    {
        
    }

    internal void Render()
    {
        
    }
}