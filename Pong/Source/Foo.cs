using Guap;

namespace Pong;

sealed class Foo : Entity
{
    protected override void OnStart() => Keyboard.OnKeyPress += k => Console.WriteLine(k);
    protected override void OnUpdate(float dt) { }
    protected override void OnRender() { }
    protected override void OnDestroy() { }
}