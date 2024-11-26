using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.State.Turn;
using Utility;

namespace BattleScene.InterfaceAdapter.Presenter
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

        public void StartAnimation(MessageCode messageCode, Context? context = null, bool noWait = false)
        {
            MyDebug.Assert(messageCode != MessageCode.NoMessage);
            _messageView.StopAnimation();
            var message = _messageResource.Get(messageCode).Message;
            var _ = _messageView.StartAnimationAsync(new MessageViewDto(message, noWait));
        }
    }
}