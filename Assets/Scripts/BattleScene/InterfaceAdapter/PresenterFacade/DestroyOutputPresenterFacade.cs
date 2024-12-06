using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacade
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

        public void OutputThenDestroyFailure()
        {
            _messageView.StartAnimation(MessageCode.NoMessage);
        }

        public void OutputThenDestroySuccess(BattleEventEntity destroyEvent)
        {
            // TODO: メッセージを設定する
            _messageView.StartAnimation(MessageCode.NoMessage);
            _playerImageView.StartAnimation(PlayerImageCode.NoImage, Slide);
        }
    }
}