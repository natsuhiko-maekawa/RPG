using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class DamageOutputFacade
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly AttackCountViewPresenter _attackCountView;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly VibrationViewPresenter _vibrationView;

        public DamageOutputFacade(
            IRepository<CharacterEntity, CharacterId> characterRepository, 
            OrderedItemsDomainService orderedItems,
            AttackCountViewPresenter attackCountView,
            DamageViewPresenter damageView,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            VibrationViewPresenter vibrationView)
        {
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
            _attackCountView = attackCountView;
            _damageView = damageView;
            _messageView = messageView;
            _playerImageView = playerImageView;
            _vibrationView = vibrationView;
        }

        public async Task Output(DamageValueObject damage)
        {
            var animationList = new List<Task>();
            
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var isActorPlayer = _characterRepository.Select(actorId).IsPlayer;
            if (isActorPlayer)
            {
                var attackCountAnimation = _attackCountView.Start();
                animationList.Add(attackCountAnimation);
            }
            else
            {
                var playerImageCode = damage.IsAvoid
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
            if (DamagesOneself(damage.AttackList)) return MessageCode.DamageOneselfMessage;
            return damage.AttacksWeakPoint
                ? MessageCode.WeakPointMessage
                : MessageCode.DamageMessage;
        }
        
        private bool DamagesOneself(IReadOnlyList<AttackValueObject> attackList) => attackList.Any(IsOneself);

        private bool IsOneself(AttackValueObject attack)
        {
            var targetId = attack.TargetId;
            _orderedItems.First().TryGetCharacterId(out var actorId);
            Debug.Assert(actorId != null);
            var value = Equals(targetId, actorId);
            return value;
        }
    }
}