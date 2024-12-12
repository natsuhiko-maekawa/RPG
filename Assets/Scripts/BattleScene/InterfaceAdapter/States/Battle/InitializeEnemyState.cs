using BattleScene.InterfaceAdapter.Presenters;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.States.Battle
{
    public class InitializeEnemyState : BaseState
    {
        private readonly InitializeEnemyUseCase _useCase;
        private readonly EnemyImageViewPresenter _enemyImageView;
        private readonly TurnState _turnState;

        public InitializeEnemyState(
            InitializeEnemyUseCase useCase,
            EnemyImageViewPresenter enemyImageView,
            TurnState turnState)
        {
            _useCase = useCase;
            _enemyImageView = enemyImageView;
            _turnState = turnState;
        }

        public override async void Start()
        {
            var enemyList = _useCase.CreateEnemy();
            _enemyImageView.StartAnimation(enemyList.Count);
            await _enemyImageView.SetImage(enemyList);
            _useCase.Initialize();
            Context.TransitionTo(_turnState);
        }
    }
}