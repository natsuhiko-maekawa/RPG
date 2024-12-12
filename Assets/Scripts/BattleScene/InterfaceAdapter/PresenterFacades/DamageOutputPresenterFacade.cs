using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.InterfaceAdapter.Presenters;
using static BattleScene.InterfaceAdapter.Presenters.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacades
{
    public class DamageOutputPresenterFacade
    {
        private readonly AttackCountViewPresenter _attackCountView;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly VibrationViewPresenter _vibrationView;

        public DamageOutputPresenterFacade(
            AttackCountViewPresenter attackCountView,
            DamageViewPresenter damageView,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            VibrationViewPresenter vibrationView)
        {
            _attackCountView = attackCountView;
            _damageView = damageView;
            _messageView = messageView;
            _playerImageView = playerImageView;
            _vibrationView = vibrationView;
        }

        public void Output(BattleEventEntity damageEvent)
        {
            var isActorPlayer = damageEvent.Actor is { IsPlayer: true };
            if (isActorPlayer)
            {
                _attackCountView.Start();
            }

            if (damageEvent.TargetList.Any(x => x.IsPlayer))
            {
                var (playerImageCode, animationMode) = damageEvent.AttackList
                    .Where(x => x.Target.IsPlayer)
                    .All(x => !x.IsHit)
                    ? (PlayerImageCode.Avoidance, Slide)
                    : (PlayerImageCode.Damaged, Vibe);
                _playerImageView.StartAnimation(playerImageCode, animationMode);
            }

            var messageCode = GetMessageCode(damageEvent);
            _messageView.StartAnimation(messageCode);
            _damageView.StartAnimation();
            _vibrationView.StartAnimation();
        }

        private MessageCode GetMessageCode(BattleEventEntity damageEvent)
        {
            if (damageEvent.AttackList
                .All(x => !x.IsHit)) return MessageCode.AvoidMessage;
            if (DamagesOneself(damageEvent)) return MessageCode.DamageOneselfMessage;
            return damageEvent.AttackList
                .Any(x => x.AttacksWeakPoint)
                ? MessageCode.WeakPointMessage
                : MessageCode.DamageMessage;
        }

        private bool DamagesOneself(BattleEventEntity damageEvent)
        {
            var value = damageEvent.AttackList
                .Select(x => x.Target.Id)
                .Contains(damageEvent.Actor!.Id);
            return value;
        }
    }
}