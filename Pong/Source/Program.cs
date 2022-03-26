using Pong;
using Guap;
using Guap.Rendering;
using Guap.Input;
using System.Numerics;

var window = new Window("PONG", 1280, 720);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
var renderer = new Renderer(camera, window);
var world = new World(window, keyboard, graphics, renderer, camera)
    .Spawn<Player, PlayerOptions>(out var player1, Configurations.PlayerOne)
    .Spawn<Player, PlayerOptions>(out var player2, Configurations.PlayerTwo)
    .OnStart(() =>
    {
        player1.Scale = Vector2.One * 250f;
        player1.Position = new(-300f, 0f);
        player2.Scale = Vector2.One * 250f;
        player2.Position = new(300f, 0f);
    });
new Game(world, window, keyboard, graphics, renderer).Start();
