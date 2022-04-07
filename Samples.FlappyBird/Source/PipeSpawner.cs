using System.Collections.Generic;
using Mirage.Utils;

namespace Samples.FlappyBird;

sealed class PipeSpawner : Entity<Player>
{
    const float SPREAD = 615f;
    const float MIN_Y = -100f;
    const float MAX_Y = 10f;
    const float DELAY = 2f;

    readonly List<Pipe> _activePipes = new();
    Player _player;
    float _timer;
    bool _isEnabled;
    float _x;

    protected override void OnConfigure(Player config) => _player = config;

    protected override void OnAwake()
    {
        World.OnKill += OnEntityKilled;
        _timer = DELAY;
        _x = Window.Bounds.Right.X + 200;
    }

    protected override void OnKill() => World.OnKill -= OnEntityKilled;

    protected override void OnUpdate(float deltaTime)
    {
        if (!_isEnabled)
            return;
        _timer -= deltaTime;
        if (_timer <= 0f)
        {
            Spawn();
            _timer = DELAY;
        }
    }

    public void Start() => _isEnabled = true;

    public void Stop()
    {
        _isEnabled = false;
        foreach (var pipe in _activePipes) 
            pipe.Stop();
    }

    public void Reset()
    {
        foreach (var pipe in _activePipes.ToArray()) 
            World.Kill(pipe);
    }

    void Spawn()
    {
        var y = RandomNumber.Between(MIN_Y, MAX_Y);
        World.Spawn<Pipe, PipeConfig>(new(_player, Direction.Up), out var bottom);
        World.Spawn<Pipe, PipeConfig>(new(_player, Direction.Down), out var top);
        bottom.Position = new(_x, y - SPREAD / 2f);
        top.Position = new(_x, y + SPREAD / 2f);
        _activePipes.Add(bottom);
        _activePipes.Add(top);
    }

    void OnEntityKilled(Entity entity)
    {
        if (entity is not Pipe pipe)
            return;
        if (_activePipes.Contains(pipe))
            _activePipes.Remove(pipe);
    }
}