using System.Collections.Generic;
using BattleScene.UseCase.View.EnemyView.OutputData;

namespace BattleScene.UseCase.View.EnemyView.OutputBoundary
{
    public interface IEnemyViewPresenter
    {
        public void Start(EnemyOutputData enemyOutputData);
    }
}