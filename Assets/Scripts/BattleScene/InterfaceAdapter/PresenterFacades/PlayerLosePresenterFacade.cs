using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.Presenters;

namespace BattleScene.InterfaceAdapter.PresenterFacades
{
    public class PlayerLosePresenterFacade
    {
        private readonly MessageViewPresenter _messageView;

        public PlayerLosePresenterFacade(
            MessageViewPresenter messageView)
        {
            _messageView = messageView;
        }

        public void Output()
        {
            _messageView.StartAnimation(MessageCode.PlayerLoseMessage);
        }
    }
}