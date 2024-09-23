using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class DamageMessageState : PrimeSkillOutputState<DamageParameterValueObject, DamageValueObject>
    {
        private readonly AttackCountViewPresenter _attackCountView;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly VibrationViewPresenter _vibrationView;
        private readonly PrimeSkillStopState<DamageParameterValueObject, DamageValueObject> _primeSkillStopState;

        public DamageMessageState(
            AttackCountViewPresenter attackCountView,
            DamageViewPresenter damageView,
            MessageViewPresenter messageView, 
            PlayerImageViewPresenter playerImageView,
            VibrationViewPresenter vibrationView,
            PrimeSkillStopState<DamageParameterValueObject, DamageValueObject> primeSkillStopState)
        {
            _attackCountView = attackCountView;
            _damageView = damageView;
            _messageView = messageView;
            _playerImageView = playerImageView;
            _vibrationView = vibrationView;
            _primeSkillStopState = primeSkillStopState;
        }

        public override void Start()
        {
            _attackCountView.Start();
            _messageView.StartDamageMessageAnimationAsync();
            // TODO: プレイヤー攻撃時は被ダメージの立ち絵を表示しないようにする
            _playerImageView.StartAnimationAsync(PlayerImageCode.Damaged);
            _damageView.StartAnimationAsync();
            _vibrationView.StartAnimationAsync();
        }
        
        public override void Select()
        {
            SkillContext.TransitionTo(_primeSkillStopState);
        }
    }
}