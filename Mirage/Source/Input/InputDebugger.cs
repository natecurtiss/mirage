using System;

namespace Mirage.Input;

public sealed class InputDebugger : Entity
{
    protected override void OnStart() => Keyboard.OnKeyPress += k => Console.WriteLine(k);
}