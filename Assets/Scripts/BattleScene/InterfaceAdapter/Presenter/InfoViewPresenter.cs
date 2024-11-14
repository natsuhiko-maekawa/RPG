using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using Utility;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class InfoViewPresenter
    {
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly InfoView _infoView;

        public InfoViewPresenter(
            IResource<MessageDto, MessageCode> messageResource,
            MessageCodeConverterService messageCodeConverter,
            InfoView infoView)
        {
            _messageResource = messageResource;
            _messageCodeConverter = messageCodeConverter;
            _infoView = infoView;
        }

        public async Task StartAnimationAsync(MessageCode messageCode)
        {
            MyDebug.Assert(messageCode != MessageCode.NoMessage);
            var message = _messageResource.Get(messageCode).Message;
            message = _messageCodeConverter.Replace(message);
            await _infoView.StartAnimationAsync(new InfoViewModel(message));
        }

        public void StopAnimation()
        {
            _infoView.StopAnimation();
        }
    }
}