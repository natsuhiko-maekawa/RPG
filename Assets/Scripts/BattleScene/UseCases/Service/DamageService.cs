using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Service
{
    public class DamageService : ISkillElementService<DamageValueObject>
    {
        private readonly DamageEvaluatorService _damageEvaluator;
        private readonly IsHitEvaluatorService _isHitEvaluator;
        private readonly AttacksWeakPointEvaluatorService _attacksWeakPointEvaluator;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IMyRandomService _myRandom;
        private readonly IHitPointService _hitPoint;

        public DamageService(
            DamageEvaluatorService damageEvaluator,
            IsHitEvaluatorService isHitEvaluator,
            AttacksWeakPointEvaluatorService attacksWeakPointEvaluator,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IMyRandomService myRandom,
            IHitPointService hitPoint)
        {
            _damageEvaluator = damageEvaluator;
            _isHitEvaluator = isHitEvaluator;
            _attacksWeakPointEvaluator = attacksWeakPointEvaluator;
            _characterRepository = characterRepository;
            _myRandom = myRandom;
            _hitPoint = hitPoint;
        }

        [Obsolete]
        public IReadOnlyList<BattleEventValueObject> GenerateBattleEvent(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DamageValueObject> damageList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            return damageList.Select(x => Generate(actorId, skillCommon, x, targetIdList)).ToList();
        }

        [Obsolete]
        private BattleEventValueObject Generate(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            DamageValueObject damage,
            IReadOnlyList<CharacterId> targetIdList)
        {
            throw new NotImplementedException();
            // var attackList = new List<AttackValueObject>();
            // for (var i = 0; i < damage.AttackNumber; ++i)
            // {
            //     var attackedTargetIdList = GetAttackedTargetIdList(targetIdList, skillCommon.Range);
            //     foreach (var attackedTargetId in attackedTargetIdList)
            //     {
            //         var attack = new AttackValueObject(
            //             amount: _damageEvaluator.Evaluate(actorId, attackedTargetId, damage),
            //             isHit: _isHitEvaluator.Evaluate(actorId, attackedTargetId, damage),
            //             attacksWeakPoint: _attacksWeakPointEvaluator
            //                 .Evaluate(actorId, attackedTargetId, damage),
            //             targetId: attackedTargetId,
            //             index: i);
            //         attackList.Add(attack);
            //     }
            // }
            //
            // attackList.Sort((x, y) => x.Index - y.Index);
            //
            // return BattleEventValueObject.CreateDamage(
            //     actorId: actorId,
            //     skillCode: skillCommon.SkillCode,
            //     attackList: attackList.ToList());
        }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> damageEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DamageValueObject> damageList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            MyDebug.Assert(damageEventList.Count == damageList.Count);
            foreach (var (battleEvent, damage) in damageEventList
                         .Zip(damageList, (battleEvent, damage) => (battleEvent, damage)))
            {
                var attackList = new List<AttackValueObject>();
                var actorId = damageEventList.Select(x => x.ActorId).First();
                for (sbyte i = 0; i < damage.AttackNumber; ++i)
                {
                    var attackedTargetIdList = GetAttackedTargetIdList(targetIdList, skillCommon.Range);
                    foreach (var attackedTargetId in attackedTargetIdList)
                    {
                        var attack = new AttackValueObject(
                            amount: _damageEvaluator.Evaluate(actorId!, attackedTargetId, damage),
                            isHit: _isHitEvaluator.Evaluate(actorId!, attackedTargetId, damage),
                            attacksWeakPoint: _attacksWeakPointEvaluator.Evaluate(actorId!, attackedTargetId, damage),
                            targetId: attackedTargetId,
                            index: i);
                        attackList.Add(attack);
                    }
                }

                attackList.Sort((x, y) => x.Index - y.Index);
                battleEvent.UpdateDamage(
                    attackList: attackList,
                    targetIdList: targetIdList);
            }
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> damageEventList)
        {
            _hitPoint.Damaged(damageEventList);
        }

        private IReadOnlyList<CharacterId> GetAttackedTargetIdList(IReadOnlyList<CharacterId> targetIdList, Range range)
        {
            var surviveTargetIdArray = targetIdList
                .Intersect(_characterRepository.Get()
                    .Where(x => x.IsSurvive)
                    .Select(x => x.Id))
                .ToArray();
            // var surviveTargetIdList = targetIdList
            //     .Where(x => _characterRepository.Get(x).IsSurvive)
            //     .ToList();
            if (range != Range.Random) return surviveTargetIdArray;
            var attackedTargetId = _myRandom.Choice(surviveTargetIdArray);
            return new[] { attackedTargetId };
        }
    }
}