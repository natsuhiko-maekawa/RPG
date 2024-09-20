using System.Collections.Generic;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.UseCases.View.CharacterVibesView.OutputBoundary;
using BattleScene.UseCases.View.CharacterVibesView.OutputData;

namespace BattleScene.InterfaceAdapter.ObsoletePresenter
{
    internal class CharacterVibesViewPresenter : ICharacterVibesViewPresenter
    {
        private readonly EnemiesView _enemiesView;
        private readonly PlayerView _playerView;

        public CharacterVibesViewPresenter(
            EnemiesView enemiesView,
            PlayerView playerView)
        {
            _enemiesView = enemiesView;
            _playerView = playerView;
        }

        public void Start(CharacterVibesOutputData outputData)
        {
            if (outputData.CharacterOutputData.IsPlayer)
                _playerView.StartPlayerVibesView();
            else
            {
                var enemyPosition = outputData.CharacterOutputData.EnemyNumber;
                _enemiesView[enemyPosition].StartVibesAnimationAsync();
            }
        }

        public void Start(IList<CharacterVibesOutputData> outputDataList)
        {
            foreach (var output in outputDataList)
                Start(output);
        }
    }
}