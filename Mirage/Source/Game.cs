using Mirage.Input;
using Mirage.Rendering;

namespace Mirage;

/// <summary>
/// The core class for creating a game with Mirage.
/// </summary>
/// <remarks>There should only be one of these created, and it should be the last thing created in the main file.
/// Make sure to start the game with <see cref="Start">Game.Start()</see>.
/// Note that nothing after <see cref="Start">Game.Start()</see> will be executed.</remarks>
public sealed class Game
{
    readonly World _world;
    readonly Window _window;
    readonly Keyboard _keyboard;
    readonly Graphics _graphics;
    readonly Renderer _renderer;

    /// <summary>
    /// Creates a new <see cref="Game"/>.
    /// </summary>
    /// <param name="world">The <see cref="World"/> for the <see cref="Game"/> to use.</param>
    /// <param name="window">The <see cref="Window"/> for the <see cref="Game"/> to use.</param>
    /// <param name="keyboard">The <see cref="Keyboard"/> for the <see cref="Game"/> to use.</param>
    /// <param name="graphics">The <see cref="Graphics"/> object for the <see cref="Game"/> to use.</param>
    /// <param name="renderer">The <see cref="Renderer"/> for the <see cref="Game"/> to use.</param>
    public Game(World world, Window window, Keyboard keyboard, Graphics graphics, Renderer renderer)
    {
        _world = world;
        _window = window;
        _keyboard = keyboard;
        _graphics = graphics;
        _renderer = renderer;
    }

    /// <summary>
    /// Starts the <see cref="Game"/>.
    /// </summary>
    /// <remarks>This will never stop executing until the <see cref="Window"/> is closed or the process is terminated.</remarks>
    public void Start() => _window.Load(() =>
    {
        _graphics.Lib = _window.CreateGL();
        _renderer.Initialize(_graphics.Lib);
        _world.Start();
    }, () =>
    {
        _world.Dispose();
        _renderer.Dispose();
        _window.Dispose();
        _graphics.Dispose();
    }, dt =>
    {
        _keyboard.Update();
        _world.Update(dt);
    }, () =>
    {
        _world.Render();
        _renderer.Display();
    }, _keyboard.Press, _keyboard.Release);
}