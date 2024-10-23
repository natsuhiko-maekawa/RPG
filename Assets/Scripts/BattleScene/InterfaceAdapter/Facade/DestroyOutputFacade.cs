using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

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

        public async Task OutputThenDestroyFailureAsync()
        {
            var animationList = new List<Task>();
            var messageAnimation = _messageView.StartAnimationAsync(MessageCode.NoMessage);
            animationList.Add(messageAnimation);

            await Task.WhenAll(animationList);
        }

        public async Task OutputThenDestroySuccessAsync(BattleEventValueObject battleEvent)
        {
            var animationList = new List<Task>();
            var messageAnimation = _messageView.StartAnimationAsync(MessageCode.NoMessage);
            animationList.Add(messageAnimation);

            var playerImageAnimation = _playerImageView.StartAnimationAsync(PlayerImageCode.NoImage);
            animationList.Add(playerImageAnimation);

            await Task.WhenAll(animationList);
        }
    }
}