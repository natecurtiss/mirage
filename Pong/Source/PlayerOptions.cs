namespace Pong;

readonly struct PlayerOptions
{
    public readonly string Texture;

    public PlayerOptions(string texture)
    {
        Texture = texture;
    }
}