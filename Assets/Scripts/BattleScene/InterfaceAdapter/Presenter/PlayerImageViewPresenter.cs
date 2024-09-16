using System;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class PlayerImageViewPresenter : IPlayerImageViewPresenter
    {
        private readonly Framework.View.PlayerView _playerView;

        public void Start(PlayerImageOutputData outputData)
        {
            throw new NotImplementedException();
        }
    }
}