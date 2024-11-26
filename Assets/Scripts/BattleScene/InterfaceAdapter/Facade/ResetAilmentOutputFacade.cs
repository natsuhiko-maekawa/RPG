using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class ResetAilmentOutputFacade
    {
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly IResource<SkillViewDto, SkillCode> _skillViewResource;

        public ResetAilmentOutputFacade(
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            IResource<SkillViewDto, SkillCode> skillViewResource)
        {
            _messageView = messageView;
            _playerImageView = playerImageView;
            _skillViewResource = skillViewResource;
        }

        public async Task OutputAsync(SkillCode skillCode)
        {
            var animationList = new List<Task>();
            _messageView.StartAnimation(MessageCode.ResetAilmentMessage);

            var playerImageCode = _skillViewResource.Get(skillCode).PlayerImageCode;
            var playerImageAnimation = _playerImageView.StartAnimationAsync(playerImageCode, Slide);
            animationList.Add(playerImageAnimation);

            await Task.WhenAll(animationList);
        }
    }
}