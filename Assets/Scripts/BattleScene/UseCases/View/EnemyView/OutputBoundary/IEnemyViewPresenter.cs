using BattleScene.UseCases.View.EnemyView.OutputData;

namespace BattleScene.UseCases.View.EnemyView.OutputBoundary
{
    public interface IEnemyViewPresenter
    {
        public void Start(EnemyOutputData enemyOutputData);
    }
}