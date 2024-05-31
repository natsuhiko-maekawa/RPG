using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.MessageView
{
    public class MessageViewPresenter : IMessageViewPresenter
    {
        private readonly IMessageView _messageView;

        public MessageViewPresenter(
            IMessageView messageView)
        {
            _messageView = messageView;
        }

        public void StartMessageView(string message, bool noWait)
        {
        }

        public void Start(MessageOutputData outputData)
        {
            _messageView.StopAnimation();
            _messageView.StartAnimation(new MessageViewDto(outputData.Message, outputData.NoWait));
        }
    }
}