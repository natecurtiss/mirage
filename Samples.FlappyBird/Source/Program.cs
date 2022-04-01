using Mirage.Input;
using Mirage.Rendering;
using Mirage.Utils;
using Samples.FlappyBird;

var window = new Window("Flappy // Mirage", 1280, 720, false, false);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
var renderer = new Renderer(camera, window);
var world = new World(window, keyboard, graphics, camera, renderer)
    .Spawn<Player>(out var player)
    .Spawn<Prop, PropConfig>(new("Assets/bg_sun.png".Find(), new(211, 211), new(0.5f), -1, window.Bounds.Extents - new Vector2(211 / 2f * 0.5f)))
    .Spawn<Prop, PropConfig>(new("Assets/bg_sky.png".Find(), new(1280, 720), new(1f), -5))
    .OnStart(world =>
    {
        BG.Create(window, world, "bottom", -2, new(1280, 277), 1f);
        BG.Create(window, world, "middle", -3, new(1280, 434), 0.5f);
        BG.Create(window, world, "top", -4, new(1280, 425), 0.2f);
        player.Start();
        
    });
new Game(world, window, keyboard, graphics, renderer).Start();