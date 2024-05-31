using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase.View.AilmentView.OutputBoundary;
using BattleScene.UseCase.View.AilmentView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.AilmentsView
{
    internal class AilmentViewPresenter : IAilmentViewPresenter
    {
        private readonly IEnemiesView _enemiesView;
        private readonly IPlayerStatusView _playerStatusView;

        public AilmentViewPresenter(
            IEnemiesView enemiesView,
            IPlayerStatusView playerStatusView)
        {
            _enemiesView = enemiesView;
            _playerStatusView = playerStatusView;
        }

        public void Start(AilmentOutputData ailmentOutputData)
        {
            if (ailmentOutputData.IsPlayer)
            {
                var playerAilmentsViewDtoList = ailmentOutputData.AilmentNumberList
                    .Select(x => new PlayerAilmentsViewDto(x))
                    .ToImmutableList();
                _playerStatusView.StartPlayerAilmentsView(playerAilmentsViewDtoList);
            }
            else
            {
                var ailmentsDtoList = ailmentOutputData.AilmentNumberList
                    .Select(x => new AilmentsDto(x))
                    .ToImmutableList();
                var enemyAilmentsViewDto = new EnemyAilmentsViewDto(ailmentOutputData.EnemyNumber, ailmentsDtoList);
                _enemiesView.StartEnemyAilmentsView(enemyAilmentsViewDto);
            }
        }

        public void Start(IList<AilmentOutputData> ailmentOutputDataList)
        {
            foreach (var ailmentOutputData in ailmentOutputDataList)
                Start(ailmentOutputData);
        }
    }
}