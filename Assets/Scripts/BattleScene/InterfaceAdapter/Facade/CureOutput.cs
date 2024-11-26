using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class CureOutput
    {
        private readonly CureViewPresenter _cureView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly IResource<SkillViewDto, SkillCode> _skillViewResource;

        public CureOutput(
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

        public async Task Output(BattleEventValueObject cure)
        {
            var animationList = new List<Task>();

            _messageView.StartAnimation(MessageCode.CureMessage);

            var playerImageCode = _skillViewResource.Get(cure.SkillCode).PlayerImageCode;
            var playerImageAnimation = _playerImageView.StartAnimationAsync(playerImageCode, Slide);
            animationList.Add(playerImageAnimation);

            var cureAnimation = _cureView.StartAnimationAsync(cure);
            animationList.Add(cureAnimation);

            await Task.WhenAll(animationList);
        }
    }
}