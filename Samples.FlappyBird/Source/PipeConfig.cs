namespace Samples.FlappyBird;

readonly struct PipeConfig
{
    public readonly Player Player;
    public readonly Direction Direction;

    public PipeConfig(Player player, Direction direction)
    {
        Player = player;
        Direction = direction;
    }
}