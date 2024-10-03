using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using UnityEngine;

namespace BattleScene.UseCases.Service
{
    public class SlipDamageGeneratorService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerDomainService _player;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IMyRandomService _myRandom;

        public SlipDamageGeneratorService(
            OrderedItemsDomainService orderedItems,
            PlayerDomainService player,
            CharacterPropertyFactoryService characterPropertyFactory,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IMyRandomService myRandom)
        {
            _orderedItems = orderedItems;
            _player = player;
            _characterPropertyFactory = characterPropertyFactory;
            _battleLogRepository = battleLogRepository;
            _myRandom = myRandom;
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
            var battleLog = _battleLogRepository.Select()
                .Where(x => x.SlipDamageCode == slipDamageCode)
                .Max();
            Debug.Assert(battleLog != null);
            var enemyId = battleLog.ActorId;
            var playerId = _player.GetId();
            var enemyIntelligence = _characterPropertyFactory.Crate(enemyId).Intelligence;
            var playerIntelligence = _characterPropertyFactory.Crate(playerId).Intelligence;
            var damageRate = _battlePropertyFactory.Create().SlipDefalutDamageRate;
            var damage = (int)(enemyIntelligence * enemyIntelligence / (float)playerIntelligence * damageRate)
                   + _myRandom.Range(0, 2);
            return damage;
        }
    }
}