using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Framework.ViewModels;
using BattleScene.Framework.Views;
using Utility;

namespace BattleScene.InterfaceAdapter.Presenters
{
    public class InfoViewPresenter
    {
        private readonly IResource<MessageDto, MessageCode> _messageResource;
        private readonly InfoView _infoView;

        public InfoViewPresenter(
            IResource<MessageDto, MessageCode> messageResource,
            InfoView infoView)
        {
            _messageResource = messageResource;
            _infoView = infoView;
        }

        public void StartAnimation(MessageCode messageCode)
        {
            MyDebug.Assert(messageCode != MessageCode.NoMessage);
            var message = _messageResource.Get(messageCode).Message;
            _infoView.StartAnimation(new InfoViewModel(message));
        }

        public void StopAnimation()
        {
            _infoView.StopAnimation();
        }
    }
}