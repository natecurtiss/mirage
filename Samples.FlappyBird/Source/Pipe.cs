using Mirage.Utils;

namespace Samples.FlappyBird;

sealed class Pipe : Entity<PipeConfig>
{
    const float SPEED = 500f;
    
    Player _player;
    Direction _direction;
    bool _isEnabled = true;

    protected override void OnConfigure(PipeConfig config)
    {
        _player = config.Player;
        _direction = config.Direction;
    }

    protected override void OnStart()
    {
        var dir = _direction == Direction.Up ? "u" : "d";
        Texture = $"Assets/pipe_{dir}.png".Find();
        Size = new(328, 550);
        Scale = new(0.6f);
        SortingOrder = 1;
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (!_isEnabled)
            return;
        Position -= new Vector2(SPEED * deltaTime, 0f);
        if (Bounds.Overlaps(_player.Bounds))
            _player.Die();
        else if (Bounds.IsCompletelyLeftOf(Window.Bounds)) 
            World.Kill(this);
    }

    public void Stop() => _isEnabled = false;
}