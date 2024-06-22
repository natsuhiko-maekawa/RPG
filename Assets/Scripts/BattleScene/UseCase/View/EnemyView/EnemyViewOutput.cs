using BattleScene.UseCase.Output.Interface;
using BattleScene.UseCase.View.EnemyView.OutputBoundary;
using BattleScene.UseCase.View.EnemyView.OutputDataFactory;

namespace BattleScene.UseCase.View.EnemyView
{
    public class EnemyViewOutput : IOutput
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