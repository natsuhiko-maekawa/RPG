using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacade
{
    public class DamageOutputPresenterFacade
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly AttackCountViewPresenter _attackCountView;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly VibrationViewPresenter _vibrationView;

        public DamageOutputPresenterFacade(
            ICollection<CharacterEntity, CharacterId> characterCollection,
            AttackCountViewPresenter attackCountView,
            DamageViewPresenter damageView,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            VibrationViewPresenter vibrationView)
        {
            _characterCollection = characterCollection;
            _attackCountView = attackCountView;
            _damageView = damageView;
            _messageView = messageView;
            _playerImageView = playerImageView;
            _vibrationView = vibrationView;
        }

        public void Output(BattleEventValueObject damage)
        {
            var isActorPlayer = _characterCollection.Get(damage.ActorId)!.IsPlayer;
            if (isActorPlayer)
            {
                _attackCountView.Start();
            }

            if (damage.ActualTargetIdList.Any(x => _characterCollection.Get(x).IsPlayer))
            {
                var (playerImageCode, animationMode) = damage.AttackList
                    .Where(x => _characterCollection.Get(x.TargetId).IsPlayer)
                    .All(x => !x.IsHit)
                    ? (PlayerImageCode.Avoidance, Slide)
                    : (PlayerImageCode.Damaged, Vibe);
                _playerImageView.StartAnimation(playerImageCode, animationMode);
            }

            var messageCode = GetMessageCode(damage);
            _messageView.StartAnimation(messageCode);
            _damageView.StartAnimation();
            _vibrationView.StartAnimation();
        }

        private MessageCode GetMessageCode(BattleEventValueObject damage)
        {
            if (damage.IsAvoid) return MessageCode.AvoidMessage;
            if (DamagesOneself(damage)) return MessageCode.DamageOneselfMessage;
            return damage.AttacksWeakPoint
                ? MessageCode.WeakPointMessage
                : MessageCode.DamageMessage;
        }

        private bool DamagesOneself(BattleEventValueObject damage)
        {
            var value = damage.AttackList
                .Any(x => Equals(x.TargetId, damage.ActorId));
            return value;
        }
    }
}