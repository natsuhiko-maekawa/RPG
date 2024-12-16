using BattleScene.Domain.Codes;
using BattleScene.Presenters.Presenters;

namespace BattleScene.Presenters.PresenterFacades
{
    public class RestoreOutputPresenterFacade
    {
        private readonly MessageViewPresenter _messageView;
        private readonly RestoreViewPresenter _restoreView;

        public RestoreOutputPresenterFacade(
            MessageViewPresenter messageView,
            RestoreViewPresenter restoreView)
        {
            _messageView = messageView;
            _restoreView = restoreView;
        }

        public void Output()
        {
            _messageView.StartAnimation(MessageCode.RestoreMessage);
            _restoreView.StartAnimation();
        }
    }
}