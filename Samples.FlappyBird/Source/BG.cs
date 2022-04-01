using Mirage.Utils;

namespace Samples.FlappyBird;

sealed class BG : Entity<BGConfig>
{
    string _texture;
    int _sortingOrder;
    Vector2 _startingPosition;
    Vector2 _size;
    float _scrollSpeed;
    
    protected override void OnConfigure(BGConfig config)
    {
        _texture = config.Texture;
        _sortingOrder = config.SortingOrder;
        _startingPosition = config.StartingPosition;
        _size = config.Size;
        _scrollSpeed = config.ScrollSpeed;
    }

    protected override void OnStart()
    {
        Texture = _texture;
        SortingOrder = _sortingOrder;
        Position = _startingPosition;
        Size = _size;
    }

    protected override void OnUpdate(float deltaTime)
    {
        Position -= new Vector2(_scrollSpeed, 0f);
        if (Bounds.IsCompletelyLeftOf(Window.Bounds))
            Position = new(_size.X - 2, Position.Y);
    }

    public static void Create(Window window, World world, string part, int order, Vector2 size, float speed)
    {
        var start = new Vector2(0f, window.Bounds.Bottom.Y + size.Y / 2f);
        var config = new BGConfig($"Assets/bg_{part}.png".Find(), order, start, size, speed);
        world.Spawn<BG, BGConfig>(config.At(start));
        world.Spawn<BG, BGConfig>(config.At(start + new Vector2(size.X - 2, 0f)));
    }
    
}