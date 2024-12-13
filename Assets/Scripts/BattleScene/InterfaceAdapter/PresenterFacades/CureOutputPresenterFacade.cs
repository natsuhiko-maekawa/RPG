using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.InterfaceAdapter.Presenters;
using static BattleScene.InterfaceAdapter.Presenters.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacades
{
    public class CureOutputPresenterFacade
    {
        private readonly CureViewPresenter _cureView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly IResource<SkillViewDto, SkillCode> _skillViewResource;

        public CureOutputPresenterFacade(
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            IResource<SkillViewDto, SkillCode> skillViewResource,
            CureViewPresenter cureView)
        {
            _messageView = messageView;
            _playerImageView = playerImageView;
            _skillViewResource = skillViewResource;
            _cureView = cureView;
        }

        public void Output(BattleEventEntity cureEvent)
        {
            _messageView.StartAnimation(MessageCode.CureMessage);

            var playerImageCode = _skillViewResource.Get(cureEvent.SkillCode).PlayerImageCode;
            _playerImageView.StartAnimation(playerImageCode, Slide);

            _cureView.StartAnimation(cureEvent);
        }
    }
}