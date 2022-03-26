using Guap.Input;

namespace Pong;

static class Configurations
{
    public static readonly PlayerOptions PlayerOne = new(new(-620f, 0f), Key.W, Key.S);
    public static readonly PlayerOptions PlayerTwo = new(new(620f, 0f), Key.UpArrow, Key.DownArrow);
}