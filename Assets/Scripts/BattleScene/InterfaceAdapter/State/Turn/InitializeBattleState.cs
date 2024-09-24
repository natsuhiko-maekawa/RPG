using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    internal class InitializeBattleState : BaseState
    {
        private readonly InitializePlayerState _initializePlayerState;
        private readonly TurnInitializerService _turnInitializer;

        public InitializeBattleState(
            InitializePlayerState initializePlayerState,
            TurnInitializerService turnInitializer)
        {
            _initializePlayerState = initializePlayerState;
            _turnInitializer = turnInitializer;
        }

        public override void Start()
        {
            _turnInitializer.Initialize();
            Context.TransitionTo(_initializePlayerState);
        }
    }
}