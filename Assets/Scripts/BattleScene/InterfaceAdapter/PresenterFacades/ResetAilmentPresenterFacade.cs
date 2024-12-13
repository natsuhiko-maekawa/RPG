using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.Presenters;
using static BattleScene.InterfaceAdapter.Presenters.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacades
{
    public class ResetAilmentPresenterFacade
    {
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly IResource<SkillViewDto, SkillCode> _skillViewResource;

        public ResetAilmentPresenterFacade(
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            IResource<SkillViewDto, SkillCode> skillViewResource)
        {
            _messageView = messageView;
            _playerImageView = playerImageView;
            _skillViewResource = skillViewResource;
        }

        public void Output(SkillCode skillCode)
        {
            _messageView.StartAnimation(MessageCode.ResetAilmentMessage);

            var playerImageCode = _skillViewResource.Get(skillCode).PlayerImageCode; 
            _playerImageView.StartAnimation(playerImageCode, Slide);
        }
    }
}