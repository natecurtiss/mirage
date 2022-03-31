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

```cs
new Game().Start();
``` 

The `Game` class takes in a few arguments in its constructor, so let's create those. Start with the `Window`, passing in a `title`, `width`, `height` and optionally whether the path to a custom `Window` `Icon`, the `Color` to use the for `Window's` background, and/or whether it should `maximized` and/or `resizable`.
```cs
var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
new Game(window).Start();
``` 

Next we'll need the other arguments, so create the `Keyboard`...
```cs
var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
var keyboard = new Keyboard();
new Game(window, keyboard).Start();
``` 

the `Graphics` object, which acts as the wrapper for OpenGL....
```cs
var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
var keyboard = new Keyboard();
var graphics = new Graphics();
new Game(window, keyboard, graphics).Start();
``` 

the `Camera`, passed in to the `Window`...
```cs
var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
new Game(window, keyboard, graphics, camera).Start();
``` 

the `Renderer`, passed in to the `Camera` and the `Window`...
```cs
var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
var renderer = new Renderer(camera, window);
new Game(window, keyboard, graphics, camera, renderer).Start();
``` 

and finally, the `World`, which contains all of the `Entities` in the `Game`. You'll need to pass in everything to this.
```cs
var window = new Window("If you can read this you don't need glasses.", 1920, 1080, maximized: true, resizable: false);
var keyboard = new Keyboard();
var graphics = new Graphics();
var camera = new Camera(window);
var renderer = new Renderer(camera, window);
var world = new World(window, keyboard, graphics, camera, renderer);
new Game(window, keyboard, graphics, camera, renderer, world).Start();
``` 

Now if we run our application we should get a blank `Window` with a title and icon!

### Basics

A "thing" in the `World` is called an `Entity`; let's create one! First let's create a new file in our project called `Player.cs`, and make that class inherit from `Entity`.
```cs
class Player : Entity
{

}
```

`Entities` have a set of "event methods" called at different times at different frequencies that can be overriden. Here's a brief explanation of all of them.
- `OnAwake()`: called BEFORE the first frame of the `Entity`'s lifetime; use this for initializing variables and event handling
- `OnStart()`: called ON the first frame of the `Entity`'s lifetime; use this for game logic that should run on the first frame
- `OnDestroy()`: called when the `Entity` is destroyed with `World.Kill()`
- `OnUpdate(float deltaTime)`: called every frame

Simply override any of the event methods to have your `Entity` receive callbacks.
```cs
class Player : Entity
{
  protected override void OnStart()
  {
    Console.WriteLine("The Game has started lol.");
  }
  
  protected override void OnUpdate()
  {
    Console.WriteLine("The Game has updated lmao.");
  }
}
```

Spawning an `Entity` is just as easy. To spawn an `Entity` we need to go through the `World` first, as that's where `Entities` live. Back in our `Program.cs` file we have a reference to the `World`, so let's spawn in our `Player` there.
```cs
var world = new World(window, keyboard, graphics, camera, renderer).Spawn<Player>();
``` 

That's it! The `Spawn<T>()` method takes in a type parameter `T`, which is just the type of `Entity` we'd like to spawn (in this case: `Player`). `World.Spawn<T>()`returns the `World` so that we can chain these as much as we want, which makes it look hella pretty.
```cs
var world = new World(window, keyboard, graphics, camera, renderer)
  .Spawn<Player>()
  .Spawn<Enemy>()
  .Spawn<Enemy>()
  .Spawn<Floor>()
  .Spawn<GameManager>()
  // ...
``` 

### Resolving Dependencies
In literally every single video game ever developed, objects depend on each other. Mirage is code-only, so there's no drag-and-drop visual editor like Unity or Godot, but there are still a few good ways to resolve dependencies.

1. After Spawning

There's an overload for `World.Spawn<T>()` that takes in an argument to output the spawned `Entity` of type `T`.
```cs
var world = new World(window, keyboard, graphics, camera, renderer)
  .Spawn<Enemy>(out var enemy) // The Enemy needs to know where the Player is to follow them.
  .Spawn<Player>(out var player);
```

We can then do stuff to this `Entity` by chaining a `World.OnAwake()` or `World.OnStart()` call (generally you'll want to use the first two). These two methods act just like `Entity.OnAwake()` `Entity.OnStart()`, but are called after every single `Entity` has received the callback for the respective event method.
```cs
var world = new World(window, keyboard, graphics, camera, renderer)
  .Spawn<Enemy>(out var enemy)
  .Spawn<Player>(out var player)
  .OnAwake() // Called after Enemy.OnAwake() and Player.OnAwake().
  {
    enemy.Target = player;
  };
```

Note: a `World.OnUpdate(float deltaTime)` callback also exists.

2. Before Spawning

Sometimes you'll want to pass in a lot of simple values, and something like 
```cs
var world = new World(window, keyboard, graphics, camera, renderer)
  .Spawn<Player>(out var player)
  .OnAwake()
  {
    player.Speed = 1f;
    player.Jump = 5.5f;
    player.Height = 20f;
    player.ShouldLick = true;
    player.IsSubscribedToN8Dev = true;
    player.Pants = new Pants(Jeans.Good);
    player.EyeColor = Eyes.Green;
    // ...
  };
```

just isn't gonna cut it.

Instead we can have `Player` inherit from `Entity<T>` like so.
```cs
class Player : Entity<float>
{
    
}
``` 

This gives us an extra method, `OnConfigure(T config)` which looks like this in our `Player`.
```cs
class Player : Entity<float>
{
  protected override void OnConfigure(float config)
  {
  
  }
}
``` 

This method is special because it allows us to pass values in when we spawn in the `Entity`. In this case we're passing in the `_moveSpeed` of the player, so we'd use it like this.
```cs
class Player : Entity<float>
{
  float _moveSpeed;
  
  protected override void OnConfigure(float config)
  {
    _moveSpeed = config;
  }
}
``` 

To pass in our `_moveSpeed` we'll need to go back to our main file and use a different overload of the `Spawn<T>()` method.
```cs
var world = new World(window, keyboard, graphics, camera, renderer)
  .Spawn<Player, float>(5f);
```

All we're doing here is telling the `World` that 
- A: we're spawning in an `Entity` of type `Player`
- B: we're passing in a `float` to its `OnConfigure` method
- and C: we want that `float` to be equal to 5

And now we've given our `Player` a speed of 5! So now if we have multiple `Players` we can easily tweak values to our liking.
```cs
var world = new World(window, keyboard, graphics, camera, renderer)
  .Spawn<Player, float>(1f)
  .Spawn<Player, float>(0.5f); // Player 2 will be slower.
  .Spawn<Player, float(100f); // Player 3 just drank some Red Bull.
```

Here's that example with the enemy from earlier.
```cs
var world = new World(window, keyboard, graphics, camera, renderer)
  .Spawn<Player>(out var player)
  .Spawn<Enemy, Player>(player);
```

Much more elegant.

"But Nate..." I hear you ask, "What if I have, for example, a bunch of weapons that have multiple properties I'd like to tweak per instance? This way only allows me to pass in a single argument to an `Entity`." 

Well, you're right...in a way, but there's a pretty simple workaround. To fix this we can just create a struct that's something like this.
```cs
struct WeaponConfig
{
  public float Power;
  public float Range;
  // ...
}
```

And pass THAT into our `Entity`.

```cs
class Weapon : Entity<WeaponConfig>
{
  float _name;
  float _power;
  float _range;
    
  protected override void OnConfigure(WeaponConfig config)
  {
    _power = config.Power;
    _range = config.Range;
  }
}
```

```cs
var world = new World(window, keyboard, graphics, camera, renderer)
  .Spawn<Weapon, WeaponConfig>(new("sword", 100f, 5f))
  .Spawn<Weapon, WeaponConfig>(new("spear", 30f, 30f))
  .Spawn<Weapon, WeaponConfig>(new("club", 200f, 2f))
  // ...
```

Even better, we can still get the `Entity` spawned through another overload of the `Spawn<TE, TC>()` method.
```cs
var world = new World(window, keyboard, graphics, camera, renderer)
  .Spawn<Weapon, WeaponConfig>(new("sword", 100f, 5f), out var sword)
  .Spawn<Player, Weapon>(sword);
```

## Dependencies

- [Silk.NET.Input](https://www.nuget.org/packages/Silk.NET.Input/)
- [Silk.NET.Windowing](https://www.nuget.org/packages/Silk.NET.Windowing/)
- [Silk.NET.OpenGL](https://www.nuget.org/packages/Silk.NET.OpenGL/)
- [System.Drawing.Common](https://www.nuget.org/packages/System.Drawing.Common/)

## License

Mirage is under the [MIT License](https://github.com/natecurtiss/mirage/blob/main/LICENSE.md) which gives you the freedom to do pretty much whatever you want with the engine; every game you make with Mirage is 100% yours down to the very last semicolon.
