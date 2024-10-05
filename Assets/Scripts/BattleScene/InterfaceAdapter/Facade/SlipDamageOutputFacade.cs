using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class SlipDamageOutputFacade
    {
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IResource<SkillPropertyDto, SkillCode> _skillViewResource;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly VibrationViewPresenter _vibrationView;

        public SlipDamageOutputFacade(
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IResource<SkillPropertyDto, SkillCode> skillViewResource,
            DamageViewPresenter damageView,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            VibrationViewPresenter vibrationView)
        {
            _skillFactory = skillFactory;
            _skillViewResource = skillViewResource;
            _damageView = damageView;
            _messageView = messageView;
            _playerImageView = playerImageView;
            _vibrationView = vibrationView;
        }

        public async Task Output(SkillCode skillCode)
        {
            var animationList = new List<Task>();

            var playerImageCode = _skillViewResource.Get(skillCode).PlayerImageCode;
            var playerImageAnimation = _playerImageView.StartAnimationAsync(playerImageCode);
            animationList.Add(playerImageAnimation);

            var messageCode = _skillFactory.Create(skillCode).SkillCommon.MessageCode;
            var messageAnimation = _messageView.StartAnimationAsync(messageCode);
            animationList.Add(messageAnimation);

            var damageAnimation = _damageView.StartAnimationAsync();
            animationList.Add(damageAnimation);

            var vibrationAnimation = _vibrationView.StartAnimationAsync();
            animationList.Add(vibrationAnimation);

            await Task.WhenAll(animationList);
        }
    }
}