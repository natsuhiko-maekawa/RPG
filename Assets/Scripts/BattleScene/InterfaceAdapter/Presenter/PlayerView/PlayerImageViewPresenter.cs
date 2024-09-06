using System;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputData;

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