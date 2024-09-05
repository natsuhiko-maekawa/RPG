using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility.Interface;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Service
{
    public class DamageGeneratorService
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly DamageEvaluatorService _damageEvaluator;
        private readonly IsHitEvaluatorService _isHitEvaluator;
        private readonly AttacksWeakPointEvaluatorService _attacksWeakPointEvaluator;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRandomEx _randomEx;

        public DamageGeneratorService(
            OrderedItemsDomainService orderedItems,
            DamageEvaluatorService damageEvaluator,
            IsHitEvaluatorService isHitEvaluator,
            AttacksWeakPointEvaluatorService attacksWeakPointEvaluator,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRandomEx randomEx)
        {
            _orderedItems = orderedItems;
            _damageEvaluator = damageEvaluator;
            _isHitEvaluator = isHitEvaluator;
            _attacksWeakPointEvaluator = attacksWeakPointEvaluator;
            _characterRepository = characterRepository;
            _randomEx = randomEx;
        }

        public DamageValueObject Generate(
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter,
            IList<CharacterId> targetIdList)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var attackList = new List<AttackValueObject>();
            for (var i = 0; i < damageParameter.AttackNumber; ++i)
            {
                var attackedTargetIdList = GetAttackedTargetIdList(targetIdList, skillCommon.Range);
                foreach (var attackedTargetId in attackedTargetIdList)
                {
                    var attack = new AttackValueObject(
                        amount: _damageEvaluator.Evaluate(actorId, attackedTargetId, damageParameter),
                        isHit: _isHitEvaluator.Evaluate(actorId, attackedTargetId, damageParameter),
                        attacksWeakPoint: _attacksWeakPointEvaluator
                            .Evaluate(actorId, attackedTargetId, damageParameter),
                        targetId: attackedTargetId,
                        number: i);
                    attackList.Add(attack);
                }
            }

            attackList.Sort((x, y) => x.Number - y.Number);
            
            return new DamageValueObject(
                actorId,
                skillCommon.SkillCode,
                attackList.ToImmutableList());
        }

        private ImmutableList<CharacterId> GetAttackedTargetIdList(IList<CharacterId> targetIdList, Range range)
        {
            var surviveTargetIdList = targetIdList
                .Where(x => _characterRepository.Select(x).IsSurvive)
                .ToImmutableList();
            if (range != Range.Random) return surviveTargetIdList;
            var attackedTargetId = _randomEx.Choice(surviveTargetIdList);
            return ImmutableList.Create(attackedTargetId);
        }
        
        public ResultEntity Execute(SkillEntity skill)
        {
            throw new NotImplementedException();
        }
    }
}