using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class ResetOutputFacade
    {
        private readonly MessageViewPresenter _messageView;

        public ResetOutputFacade(
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