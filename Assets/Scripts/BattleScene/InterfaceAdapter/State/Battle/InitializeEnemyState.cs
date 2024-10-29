using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class InitializeEnemyState : BaseState
    {
        private readonly InitializeEnemyUseCase _useCase;
        private readonly EnemyImagePresenter _enemyImage;
        private readonly TurnState _turnState;

        public InitializeEnemyState(
            InitializeEnemyUseCase useCase,
            EnemyImagePresenter enemyImage,
            TurnState turnState)
        {
            _useCase = useCase;
            _enemyImage = enemyImage;
            _turnState = turnState;
        }

        public override void Start()
        {
            _useCase.Initialize();
            _enemyImage.Show();
            Context.TransitionTo(_turnState);
        }
    }
}