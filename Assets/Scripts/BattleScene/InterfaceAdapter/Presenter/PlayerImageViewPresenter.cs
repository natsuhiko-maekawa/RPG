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
        private readonly IResource<PlayerImageDto, PlayerImageCode> _playerImagePathResource;
        private readonly PlayerView _playerView;

        public PlayerImageViewPresenter(
            IResource<PlayerImageDto, PlayerImageCode> playerImagePathResource,
            PlayerView playerView)
        {
            _playerImagePathResource = playerImagePathResource;
            _playerView = playerView;
        }

        public async Task StartAnimationAsync(PlayerImageCode playerImageCode, AnimationMode animationMode)
        {
            var playerImagePath = _playerImagePathResource.Get(playerImageCode).Path;
            var dto = new PlayerViewModel(playerImagePath);
            await _playerView.StartAnimation(dto);
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