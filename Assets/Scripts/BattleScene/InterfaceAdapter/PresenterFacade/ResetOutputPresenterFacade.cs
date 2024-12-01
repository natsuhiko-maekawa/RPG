using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.PresenterFacade
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
            _messageView.StartAnimation(MessageCode.NoMessage);
        }
    }
}