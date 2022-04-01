using System.Linq;
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
    .Spawn<Events>(out var events)
    .Spawn<Player>(out var player)
    .Spawn<Prop, PropConfig>(new("Assets/bg_sky.png".Find(), new(1280, 720), new(1f), -5))
    .OnStart(world =>
    {
        BG.Create(window, world, "bottom", -2, new(1280, 277), 1f, out var bg1);
        BG.Create(window, world, "middle", -3, new(1280, 434), 0.5f, out var bg2);
        BG.Create(window, world, "top", -4, new(1280, 425), 0.2f, out var bg3);
        bg1.Concat(bg2).Concat(bg3).ToList().ForEach(bg =>
        {
            events.OnStart += bg.Start;
            events.OnReset += bg.Reset;
        });
        events.OnStart += player.Start;
        events.OnReset += player.Reset;
        player.OnDie += events.Reset;
    });
new Game(world, window, keyboard, graphics, renderer).Start();