using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using Utility;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.UseCases.Services
{
    public class DamageService : ISkillElementService<DamageValueObject>
    {
        private readonly DamageEvaluatorService _damageEvaluator;
        private readonly IsHitEvaluatorService _isHitEvaluator;
        private readonly AttacksWeakPointEvaluatorService _attacksWeakPointEvaluator;
        private readonly IMyRandomService _myRandom;
        private readonly IHitPointService _hitPoint;

        public DamageService(
            DamageEvaluatorService damageEvaluator,
            IsHitEvaluatorService isHitEvaluator,
            AttacksWeakPointEvaluatorService attacksWeakPointEvaluator,
            IMyRandomService myRandom,
            IHitPointService hitPoint)
        {
            _damageEvaluator = damageEvaluator;
            _isHitEvaluator = isHitEvaluator;
            _attacksWeakPointEvaluator = attacksWeakPointEvaluator;
            _myRandom = myRandom;
            _hitPoint = hitPoint;
        }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> damageEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DamageValueObject> damageList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            MyDebug.Assert(damageEventList.Count == damageList.Count);
            foreach (var (battleEvent, damage) in damageEventList
                         .Zip(damageList, (battleEvent, damage) => (battleEvent, damage)))
            {
                var attackList = new List<AttackValueObject>();
                var actor = damageEventList.Select(x => x.Actor).First();
                actor = actor ?? throw new InvalidOperationException();
                for (sbyte i = 0; i < damage.AttackCount; ++i)
                {
                    var attackedTargetList = GetAttackedTargetList(targetList, skillCommon.Range);
                    foreach (var attackedTarget in attackedTargetList)
                    {
                        var attack = new AttackValueObject(
                            amount: _damageEvaluator.Evaluate(actor, attackedTarget, damage),
                            isHit: _isHitEvaluator.Evaluate(actor, attackedTarget, damage),
                            attacksWeakPoint: _attacksWeakPointEvaluator.Evaluate(actor, attackedTarget, damage),
                            target: attackedTarget,
                            index: i);
                        attackList.Add(attack);
                    }
                }

                attackList.Sort((x, y) => x.Index - y.Index);
                battleEvent.UpdateDamage(
                    attackList: attackList,
                    targetList: targetList);
            }
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> damageEventList)
        {
            _hitPoint.Damaged(damageEventList);
        }

        private IReadOnlyList<CharacterEntity> GetAttackedTargetList(IReadOnlyList<CharacterEntity> targetList, Range range)
        {
            var surviveTargetArray = targetList
                .Where(x => x.IsSurvive)
                .ToArray();
            if (range != Range.Random) return surviveTargetArray;
            var attackedTargetId = _myRandom.Choice(surviveTargetArray);
            return new[] { attackedTargetId };
        }
    }
}