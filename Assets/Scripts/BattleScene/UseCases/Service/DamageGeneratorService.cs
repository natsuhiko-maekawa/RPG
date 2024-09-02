using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class DamageGeneratorService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly DamageEvaluatorService _damageEvaluator;
        private readonly IsHitEvaluatorService _isHitEvaluator;
        private readonly AttacksWeakPointEvaluatorService _attacksWeakPointEvaluator;
        private readonly TargetDomainService _target;

        public DamageGeneratorService(
            OrderedItemsDomainService orderedItems,
            DamageEvaluatorService damageEvaluator,
            IsHitEvaluatorService isHitEvaluator,
            AttacksWeakPointEvaluatorService attacksWeakPointEvaluator,
            TargetDomainService target)
        {
            _orderedItems = orderedItems;
            _damageEvaluator = damageEvaluator;
            _isHitEvaluator = isHitEvaluator;
            _attacksWeakPointEvaluator = attacksWeakPointEvaluator;
            _target = target;
        }

        public DamageValueObject Generate(
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var damageList = new List<AttackValueObject>();
            for (var i = 0; i < damageParameter.AttackNumber; ++i)
            {
                var targetIdList = _target.Get(actorId, skillCommon.Range);
                foreach (var targetId in targetIdList)
                {
                    var damage = new AttackValueObject(
                        amount: _damageEvaluator.Evaluate(actorId, targetId, damageParameter),
                        isHit: _isHitEvaluator.Evaluate(actorId, targetId, damageParameter),
                        attacksWeakPoint: _attacksWeakPointEvaluator.Evaluate(actorId, targetId, damageParameter),
                        targetId: targetId,
                        number: i);
                    damageList.Add(damage);
                }
            }

            damageList.Sort((x, y) => x.Number - y.Number);
            
            return new DamageValueObject(
                actorId,
                skillCommon.SkillCode,
                damageList.ToImmutableList());
        }
        
        public ResultEntity Execute(SkillEntity skill)
        {
            throw new NotImplementedException();
        }
    }
}