using Mirage.Utils;
using Mirage.Utils.FSM;
using static Samples.Pong.PlayerIndex;
using static Samples.Pong.PlayerState;

namespace Samples.Pong;

sealed class Player : Entity<PlayerConfig>
{
    FiniteStateMachine<PlayerState> _fsm;
    PlayerConfig _config;
    PlayerIndex _index;

    protected override void OnConfigure(PlayerConfig config)
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
        Size = new(10f, 100f);
    }

    protected override void OnUpdate(float deltaTime) => _fsm.Update(deltaTime);
}