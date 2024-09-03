using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.MessageView
{
    public class MessageViewPresenter : IMessageViewPresenter
    {
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly IMessageView _messageView;

        public MessageViewPresenter(
            IResource<MessageDto, MessageCode> messageResource,
            MessageCodeConverterService messageCodeConverter,
            IMessageView messageView)
        {
            _messageResource = messageResource;
            _messageCodeConverter = messageCodeConverter;
            _messageView = messageView;
        }

        public void StartMessageView(string message, bool noWait)
        {
        }

        public void Start(MessageCode messageCode, bool noWait = false)
        {
            _messageView.StopAnimation();
            var message = _messageResource.Get(messageCode);
            var replacedMessage = _messageCodeConverter.Replace(message.Message);
            _messageView.StartAnimation(new MessageViewDto(replacedMessage, noWait));
        }
        
        public void Start(MessageOutputData outputData)
        {
            _messageView.StopAnimation();
            var message = _messageCodeConverter.ToMessage(outputData.MessageCode);
            _messageView.StartAnimation(new MessageViewDto(message, outputData.NoWait));
        }
    }
}