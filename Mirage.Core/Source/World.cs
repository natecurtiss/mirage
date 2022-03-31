using System.Collections.Generic;
using Mirage.Input;
using Mirage.Rendering;

namespace Mirage;

/// <summary>
/// The collection of all the <see cref="Entity">Entities</see> spawned into the <see cref="Game"/>.
/// </summary>
public sealed class World : IDisposable
{
    readonly List<Entity> _starting = new();
    readonly List<Entity> _entities = new();
    readonly List<Entity> _destroyed = new();
    readonly Graphics _graphics;
    readonly Renderer _renderer;
    readonly Camera _camera;
    readonly Window _window;
    readonly Keyboard _keyboard;

    bool _hasStarted;
    Action _onInitialize;
    Action _onStart;

    /// <summary>
    /// Creates a <see cref="World"/>.
    /// </summary>
    /// <param name="window">The <see cref="Window"/> used by the <see cref="Game"/>.</param>
    /// <param name="keyboard">The <see cref="Keyboard"/> used by the <see cref="Game"/>.</param>
    /// <param name="graphics">The <see cref="Graphics"/> object used by the <see cref="Game"/>.</param>
    /// <param name="renderer">The <see cref="Renderer"/> used by the <see cref="Game"/>.</param>
    /// <param name="camera">The <see cref="Camera"/> used by the <see cref="Game"/>.</param>
    public World(Window window, Keyboard keyboard, Graphics graphics, Renderer renderer, Camera camera)
    {
        _graphics = graphics;
        _renderer = renderer;
        _camera = camera;
        _window = window;
        _keyboard = keyboard;
    }
    
    /// <summary>
    /// Spawns an <see cref="Entity"/> in the <see cref="World"/> with a configuration passed in.
    /// </summary>
    /// <param name="config">The configuration to pass in.</param>
    /// <typeparam name="TE">The type of <see cref="Entity{T}"/> to spawn in.</typeparam>
    /// <typeparam name="TS">The type of configuration to pass in.</typeparam>
    /// <returns>The <see cref="World"/> to give us that sweet fluent API.</returns>
    public World Spawn<TE, TS>(TS config) where TE : Entity<TS>, new() => Spawn<TE, TS>(out _, config);
    
    /// <param name="config">The configuration to pass in.</param>
    /// <param name="entity">The <see cref="Entity{T}"/> that gets spawned.</param>
    /// <inheritdoc cref="Spawn{TE,TS}(TS)"/>
    public World Spawn<TE, TS>(TS config, out TE entity) where TE : Entity<TS>, new() => Spawn(out entity, config);
    
    /// <param name="entity">The <see cref="Entity{T}"/> that gets spawned.</param>
    /// <param name="config">The configuration to pass in.</param>
    /// <inheritdoc cref="Spawn{TE,TS}(TS)"/>
    public World Spawn<TE, TS>(out TE entity, TS config) where TE : Entity<TS>, new()
    {
        entity = new();
        entity.Configure(config);
        if (!_hasStarted)
        {
            _starting.Add(entity);
        }
        else
        {
            entity.Initialize(_graphics.Lib, this, _renderer, _camera, _window, _keyboard);
            _entities.Add(entity);
            entity.Start();
        }
        return this;
    }
    
    /// <summary>
    /// Spawns an <see cref="Entity"/> in the <see cref="World"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Entity"/> to spawn in.</typeparam>
    /// <returns>The <see cref="World"/> to give us that sweet fluent API.</returns>
    public World Spawn<T>() where T : Entity, new() => Spawn<T>(out _);
    
    /// <param name="entity">The <see cref="Entity"/> that gets spawned.</param>
    /// <inheritdoc cref="Spawn{T}()"/>
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

    /// <summary>
    /// Destroys the <see cref="Entity"/> passed in.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to kill.</param>
    /// <returns>The <see cref="World"/> for that sweet fluent API.</returns>
    public World Kill(Entity entity)
    {
        _entities.Remove(entity);
        _destroyed.Add(entity);
        entity.Destroy();
        return this;
    }
    
    /// <summary>
    /// Called after every <see cref="Entity"/>'s <see cref="Entity.OnAwake()"/> method is called.
    /// </summary>
    /// <returns>The <see cref="World"/> for that sweet fluent API.</returns>
    /// <remarks>Use this for things like subscribing/unsubscribing to/from events and resolving dependencies.</remarks>
    public World OnAwake(Action callback)
    {
        _onInitialize += callback;
        return this;
    }

    /// <summary>
    /// Called after every <see cref="Entity"/>'s <see cref="Entity.OnStart()"/> method is called.
    /// </summary>
    /// <returns>The <see cref="World"/> for that sweet fluent API.</returns>
    /// <remarks>Use this for logic that should happen on the first frame, like firing off events.</remarks>
    public World OnStart(Action callback)
    {
        _onStart += callback;
        return this;
    }

    /// <summary>
    /// Wakes up and starts every <see cref="Entity"/>.
    /// </summary>
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

    /// <summary>
    /// Updates every <see cref="Entity"/>.
    /// </summary>
    /// <param name="deltaTime"></param>
    internal void Update(float deltaTime)
    {
        foreach (var entity in _entities.ToArray()) 
            entity.Update(deltaTime);
    }

    /// <summary>
    /// Renders every <see cref="Entity"/>.
    /// </summary>
    internal void Render()
    {
        foreach (var entity in _entities.ToArray())
            entity.Render();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        foreach (var entity in _entities.ToArray()) 
            entity.Dispose();
        foreach (var entity in _destroyed) 
            entity.Dispose();
    }
}