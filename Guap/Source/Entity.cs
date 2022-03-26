namespace Guap;

public abstract class Entity
{
    protected abstract void OnStart();
    protected abstract void OnUpdate(float dt);
    protected abstract void OnRender();
    protected abstract void OnDestroy();

    internal void Start() => OnStart();
    internal void Update(float dt) => OnUpdate(dt);
    internal void Render() => OnRender();
    internal void Destroy() => OnDestroy();
}