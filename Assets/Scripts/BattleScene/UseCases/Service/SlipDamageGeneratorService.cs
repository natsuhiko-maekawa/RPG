using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    public class SlipDamageGeneratorService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerDomainService _player;
        private readonly IRepository<SlipDamageEntity, SlipDamageCode> _slipDamageRepository;
        private readonly IRandomEx _randomEx;

        public SlipDamageGeneratorService(
            OrderedItemsDomainService orderedItems,
            PlayerDomainService player,
            IRepository<SlipDamageEntity, SlipDamageCode> slipDamageRepository,
            IRandomEx randomEx)
        {
            _orderedItems = orderedItems;
            _player = player;
            _slipDamageRepository = slipDamageRepository;
            _randomEx = randomEx;
        }

        public SlipDamageValueObject Generate()
        {
            if (!_orderedItems.First().TryGetSlipDamageCode(out var slipDamageCode))
                throw new InvalidOperationException();

            var attack = new AttackValueObject(
                amount: GetDamageAmount(slipDamageCode),
                targetId: _player.GetId(),
                isHit: true,
                attacksWeakPoint: false,
                number: 0);

            var slipDamage = new SlipDamageValueObject(
                targetIdList: ImmutableList.Create(_player.GetId()),
                slipDamageCode: slipDamageCode,
                attackList: ImmutableList.Create(attack));
            
            return slipDamage;
        }
        
        private int GetDamageAmount(SlipDamageCode slipDamageCode)
        {
            var slipDamage = _slipDamageRepository.Select()
                .First(x => x.Id == slipDamageCode);
            var enemyIntelligence = slipDamage.EnemyIntelligence;
            var playerIntelligence = slipDamage.PlayerIntelligence;
            var damageRate = slipDamage.DamageRate;
            var damage = (int)(enemyIntelligence * enemyIntelligence / (float)playerIntelligence * damageRate)
                   + _randomEx.Range(0, 2);
            return damage;
        }
    }
}