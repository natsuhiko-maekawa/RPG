using System.Collections.Generic;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase.View.CharacterVibesView.OutputBoundary;
using BattleScene.UseCase.View.CharacterVibesView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.CharacterVibesView
{
    internal class CharacterVibesViewPresenter : ICharacterVibesViewPresenter
    {
        private readonly IEnemiesView _enemiesView;
        private readonly IPlayerView _playerView;

        public CharacterVibesViewPresenter(
            IEnemiesView enemiesView,
            IPlayerView playerView)
        {
            _enemiesView = enemiesView;
            _playerView = playerView;
        }

        public void Start(CharacterVibesOutputData outputData)
        {
            if (outputData.CharacterOutputData.IsPlayer)
                _playerView.StartPlayerVibesView();
            else
                _enemiesView.StartEnemyVibesView(new EnemyVibesViewDto(outputData.CharacterOutputData.EnemyNumber));
        }

        public void Start(IList<CharacterVibesOutputData> outputDataList)
        {
            foreach (var output in outputDataList)
                Start(output);
        }
    }
}