using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.MessageView
{
    public class MessageViewPresenter : IMessageViewPresenter
    {
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly IMessageView _messageView;

        public MessageViewPresenter(
            MessageCodeConverterService messageCodeConverter,
            IMessageView messageView)
        {
            _messageCodeConverter = messageCodeConverter;
            _messageView = messageView;
        }

        public void StartMessageView(string message, bool noWait)
        {
        }

        public void Start(MessageOutputData outputData)
        {
            _messageView.StopAnimation();
            var message = _messageCodeConverter.ToMessage(outputData.MessageCode);
            _messageView.StartAnimation(new MessageViewDto(message, outputData.NoWait));
        }
    }
}