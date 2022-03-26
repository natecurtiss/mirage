using Guap;
using Guap.Input;
using Pong;

var window = new Window("PONG", 1280, 720);
var keyboard = new Keyboard();
var world = new World(keyboard).Spawn<Foo>();
new Game(window, keyboard, world).Start();
