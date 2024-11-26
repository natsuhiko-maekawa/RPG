using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class DestroyOutputFacade
    {
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;

        public DestroyOutputFacade(
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public void OutputThenDestroyFailureAsync()
        {
            _messageView.StartAnimation(MessageCode.NoMessage);
        }

        public async Task OutputThenDestroySuccessAsync(BattleEventValueObject battleEvent)
        {
            var animationList = new List<Task>();
            _messageView.StartAnimation(MessageCode.NoMessage);

            var playerImageAnimation = _playerImageView.StartAnimationAsync(PlayerImageCode.NoImage, Slide);
            animationList.Add(playerImageAnimation);

            await Task.WhenAll(animationList);
        }
    }
}