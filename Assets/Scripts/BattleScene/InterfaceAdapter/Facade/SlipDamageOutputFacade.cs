using System;
using System.Collections.Generic;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class SlipDamageOutputFacade
    {
        private readonly IResource<SkillViewDto, SkillCode> _skillViewResource;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly VibrationViewPresenter _vibrationView;

        public SlipDamageOutputFacade(
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
            outputQueue.Enqueue(() => Output2());
            return outputQueue;
        }

        public void Output1(SkillValueObject skill)
        {
            var playerImageCode = _skillViewResource.Get(skill.Common.SkillCode).PlayerImageCode;
            _playerImageView.StartAnimation(playerImageCode, Vibe);

            var messageCode = skill.Common.AttackMessageCode;
            _messageView.StartAnimation(messageCode);
        }

        public void Output2()
        {
            _messageView.StartAnimation(MessageCode.SlipDamageMessage);
            _damageView.StartAnimation();
            _vibrationView.StartAnimation();
        }
    }
}