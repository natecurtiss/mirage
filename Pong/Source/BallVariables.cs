namespace Pong;

readonly struct BallVariables
{
    public readonly float Speed;
    public readonly float SpeedMultiplier;
    public readonly float MinBounceTilt;
    public readonly float MaxBounceTilt;

    public BallVariables(float speed, float speedMultiplier, float minBounceTilt, float maxBounceTilt)
    {
        Speed = speed;
        SpeedMultiplier = speedMultiplier;
        MinBounceTilt = minBounceTilt;
        MaxBounceTilt = maxBounceTilt;
    }
}