using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;
using Utility;

namespace BattleScene.Presenters.Presenters
{
    public class MessageViewPresenter
    {
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly MessageView _messageView;

        public MessageViewPresenter(
            IResource<MessageDto, MessageCode> messageResource,
            MessageView messageView)
        {
            _messageResource = messageResource;
            _messageView = messageView;
        }

        public void StartAnimation(MessageCode messageCode, bool noWait = false)
        {
            MyDebug.Assert(messageCode != MessageCode.NoMessage);
            var message = _messageResource.Get(messageCode).Message;
            _messageView.StartAnimation(new MessageViewModel(message, noWait));
        }
    }
}