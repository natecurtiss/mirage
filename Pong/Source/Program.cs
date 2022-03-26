using Guap;
using Guap.Input;

var window = new Window(1280, 720);
var keyboard = new Keyboard();
var world = new World();
new Game(window, keyboard, world).Start();
