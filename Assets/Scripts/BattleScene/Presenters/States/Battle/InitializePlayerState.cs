using BattleScene.UseCases.UseCases;

namespace BattleScene.Presenters.States.Battle
{
    public class InitializePlayerState : BaseState
    {
        private readonly InitializePlayerUseCase _useCase;
        private readonly InitializeEnemyState _initializeEnemyState;

        public InitializePlayerState(
            InitializePlayerUseCase useCase,
            InitializeEnemyState initializeEnemyState)
        {
            _useCase = useCase;
            _initializeEnemyState = initializeEnemyState;
        }

        public override void Start()
        {
            _useCase.Initialize();
            Context.TransitionTo(_initializeEnemyState);
        }
    }
}