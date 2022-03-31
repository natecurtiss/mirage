<p align="center">
  <a href="https://github.com/natecurtiss/mirage">
    <img src="Mirage/Assets/Textures/logo_wide_transparent.png" width="750" alt="Mirage Logo">
  </a>
</p>

## Mirage

**Mirage is a small 2D game engine written in 24 hours, because why not?** It's definitely not perfect but it's usable, and it's the first complete game engine I've ever made.

Support me: https://www.patreon.com/n8dev

Watch the devlog: https://youtube.con/n8dev

Join my Discord server for help: https://discord.gg/f8B6WW7YrD

## Specifications

- Runs on Windows exclusively because of [System.Drawing](https://www.nuget.org/packages/System.Drawing.Common/) :/
- Built and used with C# and .NET 6
- 2D sprite rendering with OpenGL ([Silk.NET bindings](https://github.com/dotnet/Silk.NET))
- Keyboard inputs
- A beautiful and easy-to-use API
- An API that's also very extendable if you wanna put in the work

## How to Install

1. Create a new .NET 6 Console application ([here's how if you don't know](https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio?pivots=dotnet-6-0))

2. Install the [NateCurtiss.Mirage](https://www.nuget.org/packages/NateCurtiss.Mirage/) NuGet package ([here's how if you've never installed NuGet packages before](https://www.youtube.com/watch?v=ohaz_sPLp4Y))

3. You're done :D

## How to Use

### Creating a Game

After adding the [NuGet package](https://www.nuget.org/packages/NateCurtiss.Mirage/) to your project, create a file called `Program.cs` with either a top-level statement or `Main` method, and then create a new `Game` and `Start()` it.

#### Program.cs

```cs
using Mirage;

new Game().Start();
``` 
#### or Program.cs
```cs
using Mirage;

static class Program
{
    static void Main()
    {
        new Game().Start();
    }
}
```

The `Game` class takes in a few arguments in its constructor, so let's create those. Start with the `Window`, passing in a `title`, `width`, `height` and optionally whether it should `maximized` and/or `resizable`.
#### Program.cs
```cs
using Mirage;

var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
new Game(window).Start();
``` 

Next we'll need the other arguments, so create the `Keyboard`...
#### Program.cs
```cs
using Mirage;
using Mirage.Input;

var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
var keyboard = new Keyboard();
new Game(window, keyboard).Start();
``` 

the `Graphics` object, which acts as the wrapper for OpenGL....
#### Program.cs
```cs
using Mirage;
using Mirage.Input;
using Mirage.Rendering;

var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
var keyboard = new Keyboard();
var graphics = new Graphics();
new Game(window, keyboard, graphics).Start();
``` 

the `Camera`, passing in the `Window`...
#### Program.cs
```cs
using Mirage;
using Mirage.Input;
using Mirage.Rendering;

var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
new Game(window, keyboard, graphics, camera).Start();
``` 

the `Renderer`, passing in the `Camera` and the `Window`...
#### Program.cs
```cs
using Mirage;
using Mirage.Input;
using Mirage.Rendering;

var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
var renderer = new Renderer(camera, window);
new Game(window, keyboard, graphics, camera, renderer).Start();
``` 

and finally, the `World`, which contains all of the `Entities` in the `Game`. You'll need to pass in everything to this.
#### Program.cs
```cs
using Mirage;
using Mirage.Input;
using Mirage.Rendering;

var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
var renderer = new Renderer(camera, window);
var world = new World(window, keyboard, graphics, camera, renderer);
new Game(window, keyboard, graphics, camera, renderer, world).Start();
``` 

Now if we run our application we should get a blank `Window` with a title!

## Dependencies

- [Silk.NET.Input](https://www.nuget.org/packages/Silk.NET.Input/)
- [Silk.NET.Windowing](https://www.nuget.org/packages/Silk.NET.Windowing/)
- [Silk.NET.OpenGL](https://www.nuget.org/packages/Silk.NET.OpenGL/)
- [System.Drawing.Common](https://www.nuget.org/packages/System.Drawing.Common/)

## License

Mirage is under the [MIT License](https://github.com/natecurtiss/mirage/blob/main/LICENSE.md) which gives you the freedom to do pretty much whatever you want with the engine; every game you make with Mirage is 100% yours down to the very last semicolon.
