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
    .Spawn<Ball, BallOptions>(Configurations.Ball, out var ball)
    .Spawn<Player, PlayerOptions>(Configurations.PlayerOne.And(ball), out var player1)
    .Spawn<Player, PlayerOptions>(Configurations.AI.And(ball), out var player2)
    .OnAwake(() =>
    {
        ball.OnShouldServe += player1.OnGoingToServe;
        ball.OnShouldServe += player2.OnGoingToServe;
        ball.OnServe += player1.OnWasServed;
        ball.OnServe += player2.OnWasServed;
    });
new Game(world, window, keyboard, graphics, renderer).Start();
