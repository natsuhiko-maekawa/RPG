using BattleScene.UseCases.View.EnemyView.OutputBoundary;
using BattleScene.UseCases.View.EnemyView.OutputDataFactory;

namespace BattleScene.UseCases.View.EnemyView
{
    public class EnemyViewOutput
    {
        private readonly EnemyOutputDataFactory _enemyOutputDataFactory;
        private readonly IEnemyViewPresenter _enemyView;

        public EnemyViewOutput(
            EnemyOutputDataFactory enemyOutputDataFactory,
            IEnemyViewPresenter enemyView)
        {
            _enemyOutputDataFactory = enemyOutputDataFactory;
            _enemyView = enemyView;
        }

        public void Out()
        {
            _enemyView.Start(_enemyOutputDataFactory.Create());
        }
    }
}