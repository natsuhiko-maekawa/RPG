using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Service
{
    public class DamageService : ISkillElementService<DamageValueObject>
    {
        private readonly DamageEvaluatorService _damageEvaluator;
        private readonly IsHitEvaluatorService _isHitEvaluator;
        private readonly AttacksWeakPointEvaluatorService _attacksWeakPointEvaluator;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly IMyRandomService _myRandom;
        private readonly IHitPointService _hitPoint;

        public DamageService(
            DamageEvaluatorService damageEvaluator,
            IsHitEvaluatorService isHitEvaluator,
            AttacksWeakPointEvaluatorService attacksWeakPointEvaluator,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            IMyRandomService myRandom,
            IHitPointService hitPoint)
        {
            _damageEvaluator = damageEvaluator;
            _isHitEvaluator = isHitEvaluator;
            _attacksWeakPointEvaluator = attacksWeakPointEvaluator;
            _characterCollection = characterCollection;
            _myRandom = myRandom;
            _hitPoint = hitPoint;
        }

        public BattleEventValueObject Generate(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            DamageValueObject damage,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var attackList = new List<AttackValueObject>();
            for (var i = 0; i < damage.AttackNumber; ++i)
            {
                var attackedTargetIdList = GetAttackedTargetIdList(targetIdList, skillCommon.Range);
                foreach (var attackedTargetId in attackedTargetIdList)
                {
                    var attack = new AttackValueObject(
                        amount: _damageEvaluator.Evaluate(actorId, attackedTargetId, damage),
                        isHit: _isHitEvaluator.Evaluate(actorId, attackedTargetId, damage),
                        attacksWeakPoint: _attacksWeakPointEvaluator
                            .Evaluate(actorId, attackedTargetId, damage),
                        targetId: attackedTargetId,
                        index: i);
                    attackList.Add(attack);
                }
            }

            attackList.Sort((x, y) => x.Index - y.Index);

            return BattleEventValueObject.CreateDamage(
                actorId: actorId,
                skillCode: skillCommon.SkillCode,
                attackList: attackList.ToList());
        }

        public IReadOnlyList<BattleEventValueObject> GenerateBattleEvent(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DamageValueObject> damageParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            return damageParameterList.Select(x => Generate(actorId, skillCommon, x, targetIdList)).ToList();
        }

        private IReadOnlyList<CharacterId> GetAttackedTargetIdList(IReadOnlyList<CharacterId> targetIdList, Range range)
        {
            var surviveTargetIdList = targetIdList
                .Where(x => _characterCollection.Get(x).IsSurvive)
                .ToList();
            if (range != Range.Random) return surviveTargetIdList;
            var attackedTargetId = _myRandom.Choice(surviveTargetIdList);
            return new List<CharacterId> { attackedTargetId };
        }

        public void RegisterBattleEvent(IReadOnlyList<BattleEventValueObject> damageList)
        {
            _hitPoint.Damaged(damageList);
        }
    }
}