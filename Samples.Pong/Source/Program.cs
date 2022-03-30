using Mirage;
using Mirage.Input;
using Mirage.Rendering;
using Samples.Pong;

var window = new Window("Pong // Mirage", 1280, 720, false, false);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
var renderer = new Renderer(camera, window);
var world = new World(window, keyboard, graphics, renderer, camera)
    .Spawn<Ball, BallConfig>(Configurations.Ball, out var ball)
    .Spawn<Player, PlayerConfig>(Configurations.PlayerOne.And(ball))
    .Spawn<Player, PlayerConfig>(Configurations.AI.And(ball));
new Game(world, window, keyboard, graphics, renderer).Start();
