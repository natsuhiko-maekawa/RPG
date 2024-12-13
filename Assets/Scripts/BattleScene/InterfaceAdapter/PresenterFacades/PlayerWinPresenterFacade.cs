using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.Presenters;

namespace BattleScene.InterfaceAdapter.PresenterFacades
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