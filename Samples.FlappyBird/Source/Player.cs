using Mirage.Input;
using Mirage.Utils;

namespace Samples.FlappyBird;

sealed class Player : Entity
{
    public event Action OnDie;
    
    const float STARTING_POSITION = -500f;
    const float JUMP = 700f;
    const float GRAVITY = -1200f;

    bool _isEnabled;
    float _velocity;

    protected override void OnStart()
    {
        Texture = "Assets/player.png".Find();
        Size = new(203, 184);
        Scale = new(0.5f);
        Reset();
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (!_isEnabled)
            return;
        if (Keyboard.WasPressed(Key.Space)) 
            _velocity = JUMP;
        else 
            _velocity += GRAVITY * deltaTime;
        Position += new Vector2(0, 1) * _velocity * deltaTime;
        if (!Bounds.Overlaps(Window.Bounds))
            Die();
    }

    public void Die() => OnDie?.Invoke();

    public void Reset()
    {
        Position = new(STARTING_POSITION, 0f);
        _velocity = 0f;
        _isEnabled = false;
    }
    
    public void Start() => _isEnabled = true;
}