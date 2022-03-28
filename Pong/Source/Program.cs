using Pong;
using Guap;
using Guap.Rendering;
using Guap.Input;
using Pong.Players;

var window = new Window("PONG", 1280, 720);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
var renderer = new Renderer(camera, window);
var world = new World(window, keyboard, graphics, renderer, camera)
    .Spawn<Ball, BallVariables>(Configurations.Ball, out var ball)
    .Spawn<Player, PlayerVariables>(Configurations.PlayerOne.And(ball))
    .Spawn<Player, PlayerVariables>(Configurations.AI.And(ball));
new Game(world, window, keyboard, graphics, renderer).Start();
