using BattleScene.Domain.Codes;
using BattleScene.Presenters.Presenters;

namespace BattleScene.Presenters.PresenterFacades
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