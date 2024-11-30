using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class RestoreOutputFacade
    {
        private readonly MessageViewPresenter _messageView;
        private readonly RestoreViewPresenter _restoreView;

        public RestoreOutputFacade(
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