using Guap;
using Guap.Utilities;
using Guap.Utilities.FSM;

namespace Pong;

sealed class Player : Entity<PlayerVariables>
{
    FiniteStateMachine<PlayerState> _fsm;
    PlayerVariables _config;
    PlayerIndex _index;

    protected override void OnConfigure(PlayerVariables config)
    {
        _config = config;
        _index = config.Index;
    }

    protected override void OnAwake()
    {
        var first = _index == PlayerIndex.One ? PlayerState.MyServe : PlayerState.TheirServe;
        _fsm = new(first,
            (PlayerState.MyServe, new PlayerMyServeState(_config, this, this)),
            (PlayerState.TheirServe, new PlayerTheirServeState(_config, this)),
            (PlayerState.Play, new PlayerPlayState(_config, this, this, this)));
    }

    protected override void OnStart()
    {
        Texture = "Assets/square.png".Find();
        Scale = new(10f, 100f);
    }

    protected override void OnUpdate(float dt) => _fsm.Update(dt);
}