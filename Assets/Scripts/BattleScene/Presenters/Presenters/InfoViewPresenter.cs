using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;
using Utility;

namespace BattleScene.Presenters.Presenters
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