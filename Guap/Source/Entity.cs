using System.Numerics;
using Guap.Rendering;
using Guap.Input;
using Silk.NET.OpenGL;

namespace Guap;

public abstract class Entity : IDisposable
{
    Renderer _renderer;
    GL _gl;
    
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public Sprite Sprite { get; private set; }
    
    protected string Texture
    {
        get => Sprite.Texture.Path;
        set
        {
            if (_gl is null)
                _startingTexture = value;
            else
                Sprite = new(_gl, value, this);
        }
    }
    string _startingTexture;

    protected World World { get; private set; }
    protected Camera Camera { get; private set; }
    protected Window Window { get; private set; }
    protected Keyboard Keyboard { get; private set; }

    protected virtual void OnStart() { }
    protected virtual void OnUpdate(float dt) { }
    protected virtual void OnDestroy() { }

    public void Dispose()
    {
        Sprite?.Dispose();
        GC.SuppressFinalize(this);
    }

    internal void Initialize(GL gl, World world, Renderer renderer, Camera camera, Window window, Keyboard keyboard)
    {
        _gl = gl;
        World = world;
        _renderer = renderer;
        Camera = camera;
        Window = window;
        Keyboard = keyboard;
    }
    
    internal void Start()
    {
        Texture = _startingTexture;
        OnStart();
    }

    internal void Update(float dt) => OnUpdate(dt);
    internal void Render()
    {
        if (Sprite is not null)
            _renderer.Queue(Sprite);
    }
    internal void Destroy() => OnDestroy();
}

public abstract class Entity<T> : Entity
{
    protected virtual void OnConfigure(T settings) { }
    internal void Configure(T settings) => OnConfigure(settings);
}