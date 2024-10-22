using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Service
{
    public class DamageService : IPrimeSkillService<DamageParameterValueObject>
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly DamageEvaluatorService _damageEvaluator;
        private readonly IsHitEvaluatorService _isHitEvaluator;
        private readonly AttacksWeakPointEvaluatorService _attacksWeakPointEvaluator;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly IMyRandomService _myRandom;

        public DamageService(
            OrderedItemsDomainService orderedItems,
            DamageEvaluatorService damageEvaluator,
            IsHitEvaluatorService isHitEvaluator,
            AttacksWeakPointEvaluatorService attacksWeakPointEvaluator,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            IMyRandomService myRandom)
        {
            _orderedItems = orderedItems;
            _damageEvaluator = damageEvaluator;
            _isHitEvaluator = isHitEvaluator;
            _attacksWeakPointEvaluator = attacksWeakPointEvaluator;
            _characterCollection = characterCollection;
            _myRandom = myRandom;
        }

        public PrimeSkillValueObject Generate(
            SkillCommonValueObject skillCommon,
            DamageParameterValueObject damageParameter,
            IReadOnlyList<CharacterId> targetIdList)
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
                        index: i);
                    attackList.Add(attack);
                }
            }

            attackList.Sort((x, y) => x.Index - y.Index);

            return PrimeSkillValueObject.CreateDamage(
                actorId: actorId,
                skillCode: skillCommon.SkillCode,
                attackList: attackList.ToList());
        }

        public IReadOnlyList<PrimeSkillValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<DamageParameterValueObject> damageParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            return damageParameterList.Select(x => Generate(skillCommon, x, targetIdList)).ToList();
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
        
        public void Register(PrimeSkillValueObject damage)
        {
            var characterList = damage.DamageDictionary
                .Select(ReduceHitPoint)
                .ToList();
            _characterCollection.Add(characterList);
        }

        public void Register(IReadOnlyList<PrimeSkillValueObject> damageList)
        {
            foreach (var damage in damageList) Register(damage);
        }

        private CharacterEntity ReduceHitPoint(KeyValuePair<CharacterId, int> characterIdDamagePair)
        {
            var characterId = characterIdDamagePair.Key;
            var character = _characterCollection.Get(characterId);
            var currentHitPoint = character.CurrentHitPoint;
            var damage = characterIdDamagePair.Value;
            var reducedHitPoint = currentHitPoint - damage;
            character.CurrentHitPoint = reducedHitPoint;
            return character;
        }
    }
}