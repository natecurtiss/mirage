using Guap;

namespace Pong;

sealed class InputDebugger : Entity
{
    protected override void OnStart() => Keyboard.OnKeyPress += k => Console.WriteLine(k);
}