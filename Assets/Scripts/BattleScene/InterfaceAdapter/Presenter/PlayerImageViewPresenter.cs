using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;

namespace BattleScene.InterfaceAdapter.Presenter
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
            var dto = new PlayerViewModel(playerImagePath);
            _playerView.StartAnimation(dto);
            switch (animationMode)
            {
                case AnimationMode.Slide:
                    _playerView.StartPlayerSlideView();
                    break;
                case AnimationMode.Vibe:
                    _playerView.StartPlayerVibeView();
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