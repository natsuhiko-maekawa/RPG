using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.MessageView
{
    public class MessageViewPresenter : IMessageViewPresenter
    {
        private readonly IFactory<string, MessageCode> _messageFactory;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly IMessageView _messageView;

        public MessageViewPresenter(
            IFactory<string, MessageCode> messageFactory,
            MessageCodeConverterService messageCodeConverter,
            IMessageView messageView)
        {
            _messageFactory = messageFactory;
            _messageCodeConverter = messageCodeConverter;
            _messageView = messageView;
        }

        public void StartMessageView(string message, bool noWait)
        {
        }

        public void Start(MessageCode messageCode, bool noWait = false)
        {
            _messageView.StopAnimation();
            var message = _messageFactory.Create(messageCode);
            
        }
        
        public void Start(MessageOutputData outputData)
        {
            _messageView.StopAnimation();
            var message = _messageCodeConverter.ToMessage(outputData.MessageCode);
            _messageView.StartAnimation(new MessageViewDto(message, outputData.NoWait));
        }
    }
}