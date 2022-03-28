using System.Numerics;
using Guap;
using Guap.Utilities;
using Guap.Utilities.FSM;

namespace Pong.Players;

sealed class Player : Entity<PlayerOptions>
{
    FiniteStateMachine<PlayerState> _fsm;
    PlayerOptions _config;
    PlayerIndex _index;
    Vector2 _startingPosition;

    protected override void OnConfigure(PlayerOptions config)
    {
        _config = config;
        _index = config.Index;
        _startingPosition = config.StartingPosition; ;
    }

    protected override void OnAwake()
    {
        _fsm = new(_index == PlayerIndex.One ? PlayerState.MyServe : PlayerState.TheirServe,
            (PlayerState.MyServe, new PlayerMyServeState(_index, this, _startingPosition, Keyboard, _config.Ball, _config.ServeDelay, _config.ShouldServe)),
            (PlayerState.TheirServe, new PlayerTheirServeState(this, _startingPosition)),
            (PlayerState.Play, new PlayerPlayState(_config.MoveDirection, this, _config.Ball, Window, Keyboard, _config.Speed)));
        Texture = "Assets/square.png".Find();
        Scale = new(10f, 100f);
        Position = _startingPosition;
    }

    protected override void OnUpdate(float dt) => _fsm.Update(dt);

    public void OnGoingToServe(PlayerIndex server) => _fsm.SwitchTo(_index == server ? PlayerState.MyServe : PlayerState.TheirServe);
    public void OnWasServed() => _fsm.SwitchTo(PlayerState.Play);
}