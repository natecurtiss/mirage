using System;
using System.Numerics;
using Mirage.Input;
using Mirage.Rendering;
using Silk.NET.OpenGL;

namespace Mirage;

/// <summary>
/// An object that can be spawned in as part of the <see cref="World"/>.
/// </summary>
public abstract class Entity : IDisposable, Transform, Boundable
{
    static readonly string _variableNameHere = "<VAR>";
    static readonly string _nullError = $"You can't access {_variableNameHere} before Game.Start() is invoked. Try sticking this in a World.OnAwake() or World.OnStart() callback, or in an Entity event method, e.g, Entity.OnAwake() or Entity.OnStart().";
    
    GL _gl;
    Renderer _renderer;
    World _world;
    Camera _camera;
    Window _window;
    Keyboard _keyboard;
    Sprite _sprite;

    /// <summary>
    /// The position of the <see cref="Entity"/>'s <see cref="Sprite"/> in the <see cref="World"/>.
    /// </summary>
    public Vector2 Position { get; set; }
    
    /// <summary>
    /// The rotation of the <see cref="Entity"/>'s <see cref="Sprite"/> in the <see cref="World"/>.
    /// </summary>
    public float Rotation { get; set; }
    
    /// <summary>
    /// The size of the <see cref="Entity"/>'s <see cref="Sprite"/> in the <see cref="World"/>.
    /// </summary>
    /// <remarks>Set this to the resolution of your texture in most cases; change <see cref="Scale"/> to resize a <see cref="Sprite"/> dynamically.</remarks>
    /// <seealso cref="Scale"/>
    public Vector2 Size { get; set; } = new(1000);
    
    /// <summary>
    /// The scale of the <see cref="Entity"/>'s <see cref="Sprite"/> in the <see cref="World"/>.
    /// </summary>
    /// <remarks>This should be used to make a <see cref="Sprite"/> bigger or smaller.
    /// This is multiplied with <see cref="Size"/> to determine the final size of the <see cref="Sprite"/> in the <see cref="World"/>;
    /// set to (1, 1) by default.</remarks>
    /// <seealso cref="Size"/>
    public Vector2 Scale { get; set; } = new(1);
    
    /// <summary>
    /// The <see cref="Bounds"/> of the <see cref="Entity"/>'s <see cref="Sprite"/>.
    /// </summary>
    public Bounds Bounds => new(Position, Size);

    /// <summary>
    /// The path to the <see cref="Texture"/> used by the <see cref="Entity"/>'s <see cref="Sprite"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if you try to access this before the <see cref="Game"/> is started.</exception>
    protected string Texture
    {
        get
        {
            if (_gl is null)
                throw new InvalidOperationException(_nullError.Replace(_variableNameHere, "Entity.Texture"));
            return _sprite.Texture.Path;
        }
        set
        {
            if (_gl is null)
                throw new InvalidOperationException(_nullError.Replace(_variableNameHere, "Entity.Texture"));
            _sprite = new(value, _gl, this);
        }
    }

    /// <summary>
    /// The <see cref="SortingOrder"/> used by the <see cref="Entity"/>'s <see cref="Sprite"/>; higher numbers -> on top.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if you try to access this before the <see cref="Game"/> is started.</exception>
    protected int SortingOrder
    {
        get
        {
            if (_gl is null)
                throw new InvalidOperationException(_nullError.Replace(_variableNameHere, "Entity.Texture"));
            return _sprite.SortingOrder;
        }
        set
        {
            if (_gl is null)
                throw new InvalidOperationException(_nullError.Replace(_variableNameHere, "Entity.SortingOrder"));
            _sprite.SortingOrder = value;
        }
    }

    /// <summary>
    /// The <see cref="World"/> used by the <see cref="Game"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if you try to access this before the <see cref="Game"/> is started.</exception>
    protected World World
    {
        get
        {
            if (_world is null)
                throw new InvalidOperationException(_nullError.Replace(_variableNameHere, "Entity.World"));
            return _world;
        }
        private set => _world = value;
    }
    
    /// <summary>
    /// The <see cref="Camera"/> used by the <see cref="Game"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if you try to access this before the <see cref="Game"/> is started.</exception>
    protected Camera Camera
    {
        get
        {
            if (_camera is null)
                throw new InvalidOperationException(_nullError.Replace(_variableNameHere, "Entity.Camera"));
            return _camera;
        }
        private set => _camera = value;
    }
    
    /// <summary>
    /// The <see cref="Window"/> used by the <see cref="Game"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if you try to access this before the <see cref="Game"/> is started.</exception>
    protected Window Window
    {
        get
        {
            if (_window is null)
                throw new InvalidOperationException(_nullError.Replace(_variableNameHere, "Entity.Window"));
            return _window;
        }
        private set => _window = value;
    }
    
    /// <summary>
    /// The <see cref="Keyboard"/> used by the <see cref="Game"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if you try to access this before the <see cref="Game"/> is started.</exception>
    protected Keyboard Keyboard
    {
        get
        {
            if (_keyboard is null)
                throw new InvalidOperationException(_nullError.Replace(_variableNameHere, "Entity.Keyboard"));
            return _keyboard;
        }
        private set => _keyboard = value;
    }

    /// <summary>
    /// Called when the <see cref="Entity"/> is created and before the first frame, before <see cref="OnStart"/>.
    /// </summary>
    /// <remarks>Use this for initializing variables.</remarks>
    protected virtual void OnAwake() { }
    
    /// <summary>
    /// Called when the <see cref="Entity"/> is created and on the first frame, after <see cref="OnAwake"/>.
    /// </summary>
    /// <remarks>Use this for logic to be run on the first frame.</remarks>
    protected virtual void OnStart() { }
    
    /// <summary>
    /// Called every frame.
    /// </summary>
    /// <param name="deltaTime">The time since the last frame.</param>
    protected virtual void OnUpdate(float deltaTime) { }
    
    /// <summary>
    /// Called before the <see cref="Entity"/> is killed.
    /// </summary>
    protected virtual void OnDestroy() { }

    /// <summary>
    /// Populates the <see cref="Entity"/>'s properties meant to be obtained from the <see cref="Game"/>.
    /// </summary>
    /// <param name="gl">The OpenGL instance obtained from the <see cref="Game"/>'s <see cref="Graphics"/> object.</param>
    /// <param name="world">The <see cref="Game"/>'s <see cref="World"/>.</param>
    /// <param name="renderer">The <see cref="Game"/>'s <see cref="Renderer"/>.</param>
    /// <param name="camera">The <see cref="Game"/>'s <see cref="Camera"/>.</param>
    /// <param name="window">The <see cref="Game"/>'s <see cref="Window"/>.</param>
    /// <param name="keyboard">The <see cref="Game"/>'s <see cref="Keyboard"/>.</param>
    internal void Initialize(GL gl, World world, Renderer renderer, Camera camera, Window window, Keyboard keyboard)
    {
        _gl = gl;
        World = world;
        _renderer = renderer;
        Camera = camera;
        Window = window;
        Keyboard = keyboard;
    }

    /// <summary>
    /// Calls the <see cref="Entity"/>'s <see cref="OnAwake"/> method.
    /// </summary>
    internal void Awake() => OnAwake();
    
    /// <summary>
    /// Calls the <see cref="Entity"/>'s <see cref="OnStart"/> method.
    /// </summary>
    internal void Start() => OnStart();
    
    /// <summary>
    /// Calls the <see cref="Entity"/>'s <see cref="OnUpdate"/> method.
    /// </summary>
    /// <param name="deltaTime">The time since the last frame.</param>
    internal void Update(float deltaTime) => OnUpdate(deltaTime);
    
    /// <summary>
    /// Tells the <see cref="Renderer"/> to render the <see cref="Entity"/>'s <see cref="Sprite"/>.
    /// </summary>
    internal void Render()
    {
        if (_sprite is not null)
            _renderer.Queue(_sprite);
    }
    
    /// <summary>
    /// Called when the <see cref="Entity"/> is killed by the <see cref="World"/>.
    /// </summary>
    internal void Destroy() => OnDestroy();
    
    /// <inheritdoc />
    public void Dispose()
    {
        _sprite?.Dispose();
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// An <see cref="Entity"/> with a <see cref="Configure"/> method for passing in values.
/// </summary>
/// <typeparam name="T">The type of argument to pass in.</typeparam>
public abstract class Entity<T> : Entity
{
    /// <summary>
    /// Called before the <see cref="Game"/> is started. Exists for passing in values.
    /// </summary>
    /// <param name="config">The argument passed in.</param>
    protected virtual void OnConfigure(T config) { }
    
    /// <summary>
    /// Calls the <see cref="Entity"/>'s <see cref="OnConfigure"/> method.
    /// </summary>
    /// <param name="config">The argument to pass in.</param>
    internal void Configure(T config) => OnConfigure(config);
}