using System.Collections.Generic;
using System.Linq;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase.View.EnemyView.OutputBoundary;
using BattleScene.UseCase.View.EnemyView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.EnemyView
{
    internal class EnemyViewPresenter : IEnemyViewPresenter
    {
        private readonly IEnemiesView _enemiesView;

        public EnemyViewPresenter(
            IEnemiesView enemiesView)
        {
            _enemiesView = enemiesView;
        }
        
        public void Start(IList<EnemyOutputData> enemyOutputDataList)
        {
            var dtoList = enemyOutputDataList
                .Select(x => new EnemyViewDto(x.EnemyNumber))
                .ToList();
            _enemiesView.StartEnemyView(dtoList);
        }
    }
}