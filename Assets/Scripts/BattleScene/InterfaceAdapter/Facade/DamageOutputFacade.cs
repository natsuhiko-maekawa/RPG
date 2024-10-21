using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class DamageOutputFacade
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly AttackCountViewPresenter _attackCountView;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly VibrationViewPresenter _vibrationView;

        public DamageOutputFacade(
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

        public async Task Output(DamageValueObject damage)
        {
            var animationList = new List<Task>();

            var isActorPlayer = _characterCollection.Get(damage.ActorId)!.IsPlayer;
            if (isActorPlayer)
            {
                var attackCountAnimation = _attackCountView.Start();
                animationList.Add(attackCountAnimation);
            }

            if (damage.ActualTargetIdList.Any(x => _characterCollection.Get(x).IsPlayer))
            {
                var playerImageCode = damage.AttackList
                    .Where(x => _characterCollection.Get(x.TargetId).IsPlayer)
                    .All(x => !x.IsHit)
                    ? PlayerImageCode.Avoidance
                    : PlayerImageCode.Damaged;
                var playerImageAnimation = _playerImageView.StartAnimationAsync(playerImageCode);
                animationList.Add(playerImageAnimation);
            }

            var messageCode = GetMessageCode(damage);
            var messageAnimation = _messageView.StartAnimationAsync(messageCode);
            animationList.Add(messageAnimation);

            var damageAnimation = _damageView.StartAnimationAsync();
            animationList.Add(damageAnimation);

            var vibrationAnimation = _vibrationView.StartAnimationAsync();
            animationList.Add(vibrationAnimation);

            await Task.WhenAll(animationList);
        }

        private MessageCode GetMessageCode(DamageValueObject damage)
        {
            if (damage.IsAvoid) return MessageCode.AvoidMessage;
            if (DamagesOneself(damage)) return MessageCode.DamageOneselfMessage;
            return damage.AttacksWeakPoint
                ? MessageCode.WeakPointMessage
                : MessageCode.DamageMessage;
        }

        private bool DamagesOneself(DamageValueObject damage)
        {
            var value = damage.AttackList
                .Any(x => Equals(x.TargetId, damage.ActorId));
            return value;
        }
    }
}