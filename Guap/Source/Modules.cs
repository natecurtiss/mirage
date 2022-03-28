using Guap.Input;
using Guap.Rendering;

namespace Guap;

public interface Modules
{
    World World { get; }
    Camera Camera { get; }
    Window Window { get; }
    Keyboard Keyboard { get; }
}