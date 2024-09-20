using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageMessageState : AbstractSkillState
    {
        private readonly AttackCountViewPresenter _attackCountView;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly VibrationViewPresenter _vibrationView;
        private readonly SkillEndState _skillEndState;

        public DamageMessageState(
            AttackCountViewPresenter attackCountView,
            DamageViewPresenter damageView,
            MessageViewPresenter messageView, 
            VibrationViewPresenter vibrationView,
            SkillEndState skillEndState)
        {
            _attackCountView = attackCountView;
            _damageView = damageView;
            _messageView = messageView;
            _vibrationView = vibrationView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            _attackCountView.Start();
            _messageView.Start(MessageCode.DamageMessage);
            _damageView.StartAnimationAsync();
            _vibrationView.StartAnimationAsync();
        }
        
        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}