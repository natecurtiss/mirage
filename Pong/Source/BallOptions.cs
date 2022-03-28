namespace Pong;

readonly struct BallOptions
{
    public readonly float Speed;
    public readonly float SpeedMultiplier;
    public readonly float MinBounceTilt;
    public readonly float MaxBounceTilt;

    public BallOptions(float speed, float speedMultiplier, float minBounceTilt, float maxBounceTilt)
    {
        Speed = speed;
        SpeedMultiplier = speedMultiplier;
        MinBounceTilt = minBounceTilt;
        MaxBounceTilt = maxBounceTilt;
    }
}