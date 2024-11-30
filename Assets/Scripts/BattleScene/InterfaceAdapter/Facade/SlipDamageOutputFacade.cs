using System;
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

        public Queue<Func<Task>> GetOutputQueue(SkillValueObject skill)
        {
            var outputQueue = new Queue<Func<Task>>();
            outputQueue.Enqueue(() => Output1(skill));
            outputQueue.Enqueue(() => Task.Run(Output2));
            return outputQueue;
        }

        public Task Output1(SkillValueObject skill)
        {
            var animationList = new List<Task>();

            var playerImageCode = _skillViewResource.Get(skill.Common.SkillCode).PlayerImageCode;
            var playerImageAnimation = _playerImageView.StartAnimationAsync(playerImageCode, Vibe);
            animationList.Add(playerImageAnimation);

            var messageCode = skill.Common.AttackMessageCode;
            _messageView.StartAnimation(messageCode);

            return Task.WhenAll(animationList);
        }

        public void Output2()
        {
            _messageView.StartAnimation(MessageCode.SlipDamageMessage);
            _damageView.StartAnimation();
            _vibrationView.StartAnimation();
        }
    }
}