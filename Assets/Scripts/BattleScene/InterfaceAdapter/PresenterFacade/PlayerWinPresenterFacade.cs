using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.PresenterFacade
{
    public class PlayerWinPresenterFacade
    {
        private readonly MessageViewPresenter _messageView;

        public PlayerWinPresenterFacade(
            MessageViewPresenter messageView)
        {
            _messageView = messageView;
        }

        public void Output()
        {
            _messageView.StartAnimation(MessageCode.PlayerWinMessage);
        }
    }
}