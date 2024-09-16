using System.Collections.Generic;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.UseCases.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCases.View.HitPointBarView.OutputData;

namespace BattleScene.InterfaceAdapter.ObsoletePresenter
{
    internal class HitPointBarViewPresenter : IHitPointBarViewPresenter
    {
        private readonly EnemiesView _enemiesView;
        private readonly PlayerView _playerView;

        public HitPointBarViewPresenter(
            EnemiesView enemiesView,
            PlayerView playerView)
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
                _playerView.StartPlayerHpBarView(
                    new PlayerHpBarViewDto(new StatusBarViewDto(maxHitPoint, currentHitPoint)));
            }
            else
            {
                var enemyNumber = outputData.CharacterOutputData.EnemyNumber;
                var maxHitPoint = outputData.MaxHitPoint;
                var currentHitPoint = outputData.CurrentHitPoint;
                _enemiesView[enemyNumber].StartHitPointBarAnimationAsync(new EnemyHpBarViewDto(enemyNumber,
                    new StatusBarViewDto(maxHitPoint, currentHitPoint)));
            }
        }

        public void Start(IList<HitPointBarOutputData> outputDataList)
        {
            foreach (var hitPointBarOutputData in outputDataList)
                Start(hitPointBarOutputData);
        }
    }
}