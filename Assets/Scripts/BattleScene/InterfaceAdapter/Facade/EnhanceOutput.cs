using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class EnhanceOutput
    {
        private readonly IResource<EnhanceViewDto, EnhanceCode> _enhanceViewResource;
        private readonly MessageViewPresenter _messageView;

        public EnhanceOutput(
            MessageViewPresenter messageView,
            IResource<EnhanceViewDto, EnhanceCode> enhanceViewResource)
        {
            _messageView = messageView;
            _enhanceViewResource = enhanceViewResource;
        }

        public void Out(BattleEventValueObject enhance)
        {
            var messageCode = _enhanceViewResource.Get(enhance.EnhanceCode).MessageCode;
            _messageView.StartAnimation(messageCode);
        }
    }
}