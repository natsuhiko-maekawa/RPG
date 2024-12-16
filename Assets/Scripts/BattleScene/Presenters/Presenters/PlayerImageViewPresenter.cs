using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;

namespace BattleScene.Presenters.Presenters
{
    public class PlayerImageViewPresenter
    {
        private readonly IResource<PlayerImageDto, PlayerImageCode> _playerImageResource;
        private readonly PlayerView _playerView;

        public PlayerImageViewPresenter(
            IResource<PlayerImageDto, PlayerImageCode> playerImageResource,
            PlayerView playerView)
        {
            _playerImageResource = playerImageResource;
            _playerView = playerView;
        }

        public async Task SetImage(IReadOnlyList<PlayerImageCode> playerImageCodeList)
        {
            var playerImagePathList = playerImageCodeList
                // 毎フレーム呼ばれるような処理ではないため、デリゲートによるアロケーションは気にしない
                .Select(x => _playerImageResource.Get(x).Path)
                .ToArray();
            await _playerView.SetImage(playerImagePathList);
        }

        public void StartAnimation(PlayerImageCode playerImageCode, AnimationMode animationMode)
        {
            var playerImagePath = _playerImageResource.Get(playerImageCode).Path;
            var model = new PlayerViewModel(playerImagePath);
            _playerView.StartAnimation(model);
            switch (animationMode)
            {
                case AnimationMode.Slide:
                    _playerView.StartSlideAnimation();
                    break;
                case AnimationMode.Vibe:
                    _playerView.StartVibeAnimation();
                    break;
            }
        }

        public enum AnimationMode
        {
            Slide,
            Vibe
        }
    }
}