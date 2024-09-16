using System;
using BattleScene.UseCases.View.EnemyView.OutputData;


namespace BattleScene.UseCases.View.EnemyView.OutputBoundary
{
    [Obsolete]
    public interface IEnemyViewPresenter
    {
        public void Start(EnemyOutputData enemyOutputData);
    }
}