using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

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
            var messageAnimation = _messageView.StartAnimationAsync(MessageCode.ResetAilmentMessage);
            animationList.Add(messageAnimation);

            var playerImageCode = _skillViewResource.Get(skillCode).PlayerImageCode;
            var playerImageAnimation = _playerImageView.StartAnimationAsync(playerImageCode);
            animationList.Add(playerImageAnimation);

            await Task.WhenAll(animationList);
        }
    }
}