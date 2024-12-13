using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.InterfaceAdapter.Presenters;
using static BattleScene.InterfaceAdapter.Presenters.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacades
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
            _messageView.StartAnimation(MessageCode.NoMessage);
        }

        public void OutputWhenDestroySuccess(BattleEventEntity destroyEvent)
        {
            _messageView.StartAnimation(MessageCode.DestroyMessage);
            _playerImageView.StartAnimation(PlayerImageCode.NoImage, Slide);
        }
    }
}