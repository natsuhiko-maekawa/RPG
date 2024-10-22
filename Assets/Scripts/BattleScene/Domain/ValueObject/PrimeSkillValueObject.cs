using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class PrimeSkillValueObject
    {
        public ActionCode ActionCode { get; protected init; } = ActionCode.NoAction;
        public AilmentCode AilmentCode { get; protected init; } = AilmentCode.NoAilment;
        public BodyPartCode BodyPartCode { get; protected init; } = BodyPartCode.NoBodyPart;
        public BuffCode BuffCode { get; protected init; } = BuffCode.NoBuff;
        public SlipCode SlipCode { get; protected init; } = SlipCode.NoSlip;
        public SkillCode SkillCode { get; protected set; } = SkillCode.NoSkill;
        public CharacterId? ActorId { get; protected set; }
        public IReadOnlyList<CharacterId> TargetIdList { get; protected set; } = Array.Empty<CharacterId>();
        public IReadOnlyList<CharacterId> ActualTargetIdList { get; protected set; } = Array.Empty<CharacterId>();
        public bool IsFailure => ActualTargetIdList.Count == 0;
        public float Rate { get; protected init; }
        public int Turn { get; protected init; }
        public LifetimeCode LifetimeCode { get; protected init; } = LifetimeCode.NoLifetime;
        public int DestroyCount { get; protected init; }
        public IReadOnlyList<AttackValueObject> AttackList { get; protected set; } = Array.Empty<AttackValueObject>();

        public IReadOnlyDictionary<CharacterId, int> DamageDictionary =>
            AttackList
                .Where(x => x.IsHit)
                .GroupBy(x => x.TargetId)
                .Select(x => x
                    .Select(y => (targetId: y.TargetId, amount: y.Amount))
                    .Aggregate((y, z) => (y.targetId, y.amount + z.amount)))
                .ToDictionary(x => x.targetId, x => x.amount);

        public bool IsAvoid =>
            AttackList
                .All(x => !x.IsHit);

        public bool AttacksWeakPoint =>
            AttackList
                .Any(x => x.AttacksWeakPoint);

        public int TechnicalPoint { get; protected init; }

        public static PrimeSkillValueObject CreateAilment(
            AilmentCode ailmentCode,
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList)
        {
            var ailment = new PrimeSkillValueObject
            {
                AilmentCode = ailmentCode,
                SkillCode = skillCode,
                ActorId = actorId,
                TargetIdList = targetIdList,
                ActualTargetIdList = actualTargetIdList
            };

            return ailment;
        }
        
        public static PrimeSkillValueObject CreateDamage(
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<AttackValueObject> attackList)
        {
            var targetIdList = attackList
                .Select(x => x.TargetId)
                .Distinct()
                .ToList();
            
            var damage = new PrimeSkillValueObject
            {
                SkillCode = skillCode,
                ActorId = actorId,
                TargetIdList = targetIdList,
                ActualTargetIdList = targetIdList,
                AttackList = attackList
            };

            return damage;
        }
    }
}