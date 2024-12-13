using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Framework.ViewModels;
using BattleScene.Framework.Views;
using Utility;

namespace BattleScene.InterfaceAdapter.Presenters
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