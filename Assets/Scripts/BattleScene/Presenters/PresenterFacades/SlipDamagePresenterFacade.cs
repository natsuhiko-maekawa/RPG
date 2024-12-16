using System;
using System.Collections.Generic;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.ValueObjects;
using BattleScene.Presenters.Presenters;
using static BattleScene.Presenters.Presenters.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.Presenters.PresenterFacades
{
    public class SlipDamagePresenterFacade
    {
        private readonly IResource<SkillViewDto, SkillCode> _skillViewResource;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly VibrationViewPresenter _vibrationView;

        public SlipDamagePresenterFacade(
            IResource<SkillViewDto, SkillCode> skillViewResource,
            DamageViewPresenter damageView,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            VibrationViewPresenter vibrationView)
        {
            _skillViewResource = skillViewResource;
            _damageView = damageView;
            _messageView = messageView;
            _playerImageView = playerImageView;
            _vibrationView = vibrationView;
        }

        public Queue<Action> GetOutputQueue(SkillValueObject skill)
        {
            var outputQueue = new Queue<Action>();
            outputQueue.Enqueue(() => Output1(skill));
            outputQueue.Enqueue(() => Output2(skill));
            return outputQueue;
        }

        private void Output1(SkillValueObject skill)
        {
            var playerImageCode = _skillViewResource.Get(skill.Common.SkillCode).PlayerImageCode;
            _playerImageView.StartAnimation(playerImageCode, Slide);

            var messageCode = skill.Common.AttackMessageCode;
            _messageView.StartAnimation(messageCode);
        }

        private void Output2(SkillValueObject skill)
        {
            var playerImageCode = _skillViewResource.Get(skill.Common.SkillCode).PlayerImageCode;
            _playerImageView.StartAnimation(playerImageCode, Vibe);

            _messageView.StartAnimation(MessageCode.SlipDamageMessage);
            _damageView.StartAnimation();
            _vibrationView.StartAnimation();
        }
    }
}