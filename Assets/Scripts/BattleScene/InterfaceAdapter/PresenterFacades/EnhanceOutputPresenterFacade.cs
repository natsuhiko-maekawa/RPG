using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.InterfaceAdapter.Presenters;

namespace BattleScene.InterfaceAdapter.PresenterFacades
{
    public class EnhanceOutputPresenterFacade
    {
        private readonly IResource<EnhanceViewDto, EnhanceCode> _enhanceViewResource;
        private readonly MessageViewPresenter _messageView;

        public EnhanceOutputPresenterFacade(
            MessageViewPresenter messageView,
            IResource<EnhanceViewDto, EnhanceCode> enhanceViewResource)
        {
            _messageView = messageView;
            _enhanceViewResource = enhanceViewResource;
        }

        public void Output(BattleEventEntity enhanceEvent)
        {
            var messageCode = _enhanceViewResource.Get(enhanceEvent.EnhanceCode).MessageCode;
            _messageView.StartAnimation(messageCode);
        }
    }
}