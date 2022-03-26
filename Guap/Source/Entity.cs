using System.Numerics;
using Guap.Rendering;
using Guap.Input;
using Silk.NET.OpenGL;

namespace Guap;

public abstract class Entity : IDisposable
{
    static readonly string _variableNameHere = "<VAR>";
    static readonly string _nullError = $"You can't access {_variableNameHere} before Game.Start() is invoked. Try sticking this in a World.OnStart() callback or in an Entity event method, e.g, Entity.OnStart() or Entity.OnUpdate().";
    
    GL _gl;
    Renderer _renderer;
    World _world;
    Camera _camera;
    Window _window;
    Keyboard _keyboard;
    string _startingTexture;

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
    protected World World
    {
        get
        {
            if (_world is null)
                throw new NullReferenceException(_nullError.Replace(_variableNameHere, "Entity.World"));
            return _world;
        }
        private set => _world = value;
    }
    protected Camera Camera
    {
        get
        {
            if (_camera is null)
                throw new NullReferenceException(_nullError.Replace(_variableNameHere, "Entity.Camera"));
            return _camera;
        }
        private set => _camera = value;
    }
    protected Window Window
    {
        get
        {
            if (_window is null)
                throw new NullReferenceException(_nullError.Replace(_variableNameHere, "Entity.Window"));
            return _window;
        }
        private set => _window = value;
    }
    protected Keyboard Keyboard
    {
        get
        {
            if (_keyboard is null)
                throw new NullReferenceException(_nullError.Replace(_variableNameHere, "Entity.Keyboard"));
            return _keyboard;
        }
        private set => _keyboard = value;
    }

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