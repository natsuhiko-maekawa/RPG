using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using UnityEngine;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class SlipDamageGeneratorService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerDomainService _player;
        private readonly DamageEvaluatorService _damageEvaluator;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;

        public SlipDamageGeneratorService(
            OrderedItemsDomainService orderedItems,
            PlayerDomainService player,
            DamageEvaluatorService damageEvaluator,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection)
        {
            _orderedItems = orderedItems;
            _player = player;
            _damageEvaluator = damageEvaluator;
            _battlePropertyFactory = battlePropertyFactory;
            _battleLogCollection = battleLogCollection;
        }

        public SlipDamageValueObject Generate()
        {
            _orderedItems.First().TryGetSlipDamageCode(out var slipCode);
            MyDebug.Assert(slipCode != SlipDamageCode.NoSlipDamage);

            var battleLog = _battleLogCollection.Get()
                .Where(x => x.SlipDamageCode == slipCode)
                .Max();
            Debug.Assert(battleLog != null);
            var actorId = battleLog.ActorId;

            var targetId = _player.GetId();

            var damageRate = _battlePropertyFactory.Create().SlipDefaultDamageRate;
            var damageParameter = new DamageParameterValueObject(
                DamageRate: damageRate,
                DamageExpressionCode: DamageExpressionCode.Slip);

            var attack = new AttackValueObject(
                amount: _damageEvaluator.Evaluate(actorId, targetId, damageParameter),
                targetId: targetId,
                isHit: true,
                attacksWeakPoint: false,
                index: 0);

            var slipDamage = new SlipDamageValueObject(
                targetIdList: new List<CharacterId> { targetId },
                slipDamageCode: slipCode,
                attackList: new List<AttackValueObject> { attack });
            return slipDamage;
        }
    }
}