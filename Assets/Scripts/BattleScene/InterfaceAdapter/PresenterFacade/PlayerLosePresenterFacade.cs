using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.PresenterFacade
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