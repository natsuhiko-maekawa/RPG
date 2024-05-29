using System.Collections.Generic;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCase.View.HitPointBarView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.StatusBarView
{
    internal class HitPointBarViewPresenter : IHitPointBarViewPresenter
    {
        private readonly IEnemiesView _enemiesView;
        private readonly IPlayerView _playerView;

        public HitPointBarViewPresenter(
            IEnemiesView enemiesView,
            IPlayerView playerView)
        {
            _enemiesView = enemiesView;
            _playerView = playerView;
        }

        public void Start(HitPointBarOutputData outputData)
        {
            if (outputData.CharacterOutputData.IsPlayer)
            {
                var maxHitPoint = outputData.MaxHitPoint;
                var currentHitPoint = outputData.CurrentHitPoint;
                _playerView.StartPlayerHpBarView(new PlayerHpBarViewDto(new StatusBarViewDto(maxHitPoint, currentHitPoint)));
            }
            else
            {
                var enemyNumber = outputData.CharacterOutputData.EnemyNumber;
                var maxHitPoint = outputData.MaxHitPoint;
                var currentHitPoint = outputData.CurrentHitPoint;
                _enemiesView.StartEnemyHpBarView(new EnemyHpBarViewDto(enemyNumber, new StatusBarViewDto(maxHitPoint, currentHitPoint)));
            }
        }

        public void Start(IList<HitPointBarOutputData> outputDataList)
        {
            foreach (var hitPointBarOutputData in outputDataList)
                Start(hitPointBarOutputData);
        }
    }
}