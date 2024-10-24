using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class InitializeBattleState : BaseState
    {
        private readonly InitializePlayerState _initializePlayerState;
        private readonly TurnService _turn;

        public InitializeBattleState(
            InitializePlayerState initializePlayerState,
            TurnService turn)
        {
            _initializePlayerState = initializePlayerState;
            _turn = turn;
        }

        public override void Start()
        {
            _turn.Initialize();
            Context.TransitionTo(_initializePlayerState);
        }
    }
}