namespace Guap;

public sealed class World
{
    readonly List<Entity> _entities = new();

    public T Spawn<T>() where T : Entity, new()
    {
        var e = new T();
        _entities.Add(e);
        return e;
    }

    public void Kill(Entity entity)
    {
        _entities.Remove(entity);
        entity.Destroy();
    }

    internal void Update(float dt)
    {
        
    }

    internal void Render()
    {
        
    }
}