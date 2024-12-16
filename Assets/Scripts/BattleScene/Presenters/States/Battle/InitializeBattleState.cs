using BattleScene.UseCases.UseCases;

namespace BattleScene.Presenters.States.Battle
{
    public class InitializeBattleState : BaseState
    {
        private readonly InitializeBattleUseCase _useCase;
        private readonly InitializePlayerState _initializePlayerState;

        public InitializeBattleState(
            InitializeBattleUseCase useCase,
            InitializePlayerState initializePlayerState)
        {
            _useCase = useCase;
            _initializePlayerState = initializePlayerState;
        }

        public override void Start()
        {
            _useCase.Initialize();
            Context.TransitionTo(_initializePlayerState);
        }
    }
}