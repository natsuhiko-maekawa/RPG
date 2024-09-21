using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageMessageState : AbstractSkillState
    {
        private readonly AttackCountViewPresenter _attackCountView;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly VibrationViewPresenter _vibrationView;
        private readonly SkillEndState _skillEndState;

        public DamageMessageState(
            AttackCountViewPresenter attackCountView,
            DamageViewPresenter damageView,
            MessageViewPresenter messageView, 
            PlayerImageViewPresenter playerImageView,
            VibrationViewPresenter vibrationView,
            SkillEndState skillEndState)
        {
            _attackCountView = attackCountView;
            _damageView = damageView;
            _messageView = messageView;
            _playerImageView = playerImageView;
            _vibrationView = vibrationView;
            _skillEndState = skillEndState;
        }

        public override void Start()
        {
            _attackCountView.Start();
            _messageView.StartDamageMessageViewAsync();
            // TODO: プレイヤー攻撃時は被ダメージの立ち絵を表示しないようにする
            _playerImageView.StartAnimationAsync(PlayerImageCode.Damaged);
            _damageView.StartAnimationAsync();
            _vibrationView.StartAnimationAsync();
        }
        
        public override void Select()
        {
            SkillContext.TransitionTo(_skillEndState);
        }
    }
}