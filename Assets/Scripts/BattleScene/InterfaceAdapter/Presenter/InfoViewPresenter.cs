using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using Utility;

namespace BattleScene.InterfaceAdapter.Presenter
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

        public void StartAnimationAsync(MessageCode messageCode)
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