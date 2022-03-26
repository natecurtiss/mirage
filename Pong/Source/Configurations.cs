using Guap.Input;

namespace Pong;

static class Configurations
{
    public static readonly PaddleOptions PlayerOne = new(new(-620f, 0f), Key.W, Key.S, 500f);
    public static readonly PaddleOptions PlayerTwo = new(new(620f, 0f), Key.UpArrow, Key.DownArrow, 500f);
    public static readonly PaddleOptions AI = new(new(620f, 0f), Key.None, Key.None, 500f);
}