using System;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCase.View.PlayerImageView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.PlayerView
{
    public class PlayerImageViewPresenter : IPlayerImageViewPresenter
    {
        private readonly IPlayerView _playerView;

        public void Start(PlayerImageOutputData outputData)
        {
            throw new NotImplementedException();
        }
    }
}