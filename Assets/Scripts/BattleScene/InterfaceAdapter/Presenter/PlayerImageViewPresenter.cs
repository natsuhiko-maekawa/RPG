﻿using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Framework.View;
using PlayerViewDto = BattleScene.Framework.ViewModel.PlayerViewDto;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class PlayerImageViewPresenter
    {
        private readonly IResource<PlayerImageDto, PlayerImageCode> _playerImagePathResource;
        private readonly PlayerView _playerView;

        public PlayerImageViewPresenter(
            IResource<PlayerImageDto, PlayerImageCode> playerImagePathResource,
            PlayerView playerView)
        {
            _playerImagePathResource = playerImagePathResource;
            _playerView = playerView;
        }

        public async Task StartAnimationAsync(PlayerImageCode playerImageCode)
        {
            var playerImagePath = _playerImagePathResource.Get(playerImageCode).Path;
            var dto = new PlayerViewDto(playerImagePath);
            await _playerView.StartAnimation(dto);
        }
    }
}