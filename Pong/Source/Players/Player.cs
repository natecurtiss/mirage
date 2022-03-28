using Guap;
using Guap.Utilities;
using Guap.Utilities.FSM;
using static Pong.PlayerIndex;
using static Pong.PlayerState;

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
        var first = _index == One ? MyServe : TheirServe;
        _fsm = new(first, 
            (MyServe, new PlayerMyServeState(_config, this, Keyboard)), 
            (TheirServe, new PlayerTheirServeState(_config, this)), 
            (Play, new PlayerPlayState(_config, this, this, Window, Keyboard)));
    }

    protected override void OnStart()
    {
        Texture = "Assets/square.png".Find();
        Scale = new(10f, 100f);
    }

    protected override void OnUpdate(float dt) => _fsm.Update(dt);
}