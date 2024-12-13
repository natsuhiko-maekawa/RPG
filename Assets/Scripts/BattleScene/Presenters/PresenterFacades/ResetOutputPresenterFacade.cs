using BattleScene.Domain.Codes;
using BattleScene.Presenters.Presenters;

namespace BattleScene.Presenters.PresenterFacades
{
    public class ResetOutputPresenterFacade
    {
        private readonly MessageViewPresenter _messageView;

        public ResetOutputPresenterFacade(
            MessageViewPresenter messageView)
        {
            _messageView = messageView;
        }

        public void Output()
        {
            _messageView.StartAnimation(MessageCode.RecoveryMessage);
        }
    }
}