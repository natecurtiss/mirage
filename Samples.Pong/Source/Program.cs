using Mirage.Input;
using Mirage.Rendering;
using Samples.Pong;

var window = new Window("Pong // Mirage", 1280, 720, false, false);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
var renderer = new Renderer(camera, window);
var world = new World(window, keyboard, graphics, camera, renderer)
    .Spawn<Ball>(out var ball)
    .Spawn<Player, PlayerConfig>(new(PlayerIndex.One, -620f,  _ => 
    {
        if (keyboard.IsDown(Key.W) || keyboard.IsDown(Key.UpArrow))
            return keyboard.IsUp(Key.S) || keyboard.IsUp(Key.DownArrow) ? 1 : 0;
        if (keyboard.IsDown(Key.S) || keyboard.IsDown(Key.DownArrow))
            return -1;
        return 0;
    }, ball))
    .Spawn<Player, PlayerConfig>(new(PlayerIndex.Two, 620f, my =>
    {
        var relative = Math.Clamp(ball.Position.Y - my.Position.Y, -1, 1);
        return (int) relative;
    }, ball));
new Game(world, window, keyboard, graphics, renderer).Start();
