using System;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputData;

namespace BattleScene.InterfaceAdapter.ObsoletePresenter
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