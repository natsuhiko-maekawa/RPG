using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.Presenters;

namespace BattleScene.InterfaceAdapter.PresenterFacades
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