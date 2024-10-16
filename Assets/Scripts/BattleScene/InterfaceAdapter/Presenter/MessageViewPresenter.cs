using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.InterfaceAdapter.State.Turn;
using Utility;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class MessageViewPresenter
    {
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly MessageView _messageView;

        public MessageViewPresenter(
            IResource<MessageDto, MessageCode> messageResource, 
            MessageCodeConverterService messageCodeConverter, 
            MessageView messageView)
        {
            _messageResource = messageResource;
            _messageCodeConverter = messageCodeConverter;
            _messageView = messageView;
        }

        public async Task StartAnimationAsync(MessageCode messageCode, Context context = null, bool noWait = false)
        {
            MyDebug.Assert(messageCode != MessageCode.NoMessage);
            _messageView.StopAnimation();
            var message = _messageResource.Get(messageCode).Message;
            message = _messageCodeConverter.Replace(message, context);
            await _messageView.StartAnimationAsync(new MessageViewDto(message, noWait));
        }
    }
}