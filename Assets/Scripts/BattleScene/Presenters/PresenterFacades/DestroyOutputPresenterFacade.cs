using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Presenters.Presenters;
using static BattleScene.Presenters.Presenters.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.Presenters.PresenterFacades
{
    public class DestroyOutputPresenterFacade
    {
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;

        public DestroyOutputPresenterFacade(
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public void OutputWhenDestroyFailure()
        {
            // _messageView.StartAnimation(MessageCode.NoMessage);
        }

        public void OutputWhenDestroySuccess(BattleEventEntity destroyEvent)
        {
            _messageView.StartAnimation(MessageCode.DestroyMessage);
            _playerImageView.StartAnimation(PlayerImageCode.NoImage, Slide);
        }
    }
}