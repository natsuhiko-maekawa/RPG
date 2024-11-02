using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using Utility;

namespace BattleScene.Domain.ValueObject
{
    public class BattleEventValueObject
    {
        public BattleEventCode BattleEventCode { get; private init; } = BattleEventCode.NoAction;
        public AilmentCode AilmentCode { get; private init; } = AilmentCode.NoAilment;
        public BodyPartCode BodyPartCode { get; private init; } = BodyPartCode.NoBodyPart;
        public BuffCode BuffCode { get; private init; } = BuffCode.NoBuff;
        public EnhanceCode EnhanceCode { get; private init; } = EnhanceCode.NoEnhance;
        public SlipCode SlipCode { get; private init; } = SlipCode.NoSlip;
        public SkillCode SkillCode { get; private set; } = SkillCode.NoSkill;
        public IReadOnlyList<AilmentCode> ResetAilmentCodeList { get; private set; } = Array.Empty<AilmentCode>();
        public IReadOnlyList<SlipCode> ResetSlipCodeList { get; private set; } = Array.Empty<SlipCode>();
        public IReadOnlyList<BodyPartCode> ResetBodyPartCodeList { get; private set; } = Array.Empty<BodyPartCode>();
        public CharacterId? ActorId { get; private set; }
        public IReadOnlyList<CharacterId> TargetIdList { get; private set; } = Array.Empty<CharacterId>();
        public IReadOnlyList<CharacterId> ActualTargetIdList { get; private init; } = Array.Empty<CharacterId>();
        public bool IsFailure => ActualTargetIdList.Count == 0;
        public float Rate { get; private init; }
        public int Turn { get; private init; }
        public LifetimeCode LifetimeCode { get; private init; } = LifetimeCode.NoLifetime;
        public int DestroyCount { get; private init; }
        public IReadOnlyList<AttackValueObject> AttackList { get; private init; } = Array.Empty<AttackValueObject>();
        public IReadOnlyList<CuringValueObject> CuringList { get; private set; } = MyList<CuringValueObject>.Empty;

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

        public int TechnicalPoint { get; private init; }

        public static BattleEventValueObject CreateAilment(
            AilmentCode ailmentCode,
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList)
        {
            var ailment = new BattleEventValueObject
            {
                BattleEventCode = BattleEventCode.Skill,
                AilmentCode = ailmentCode,
                SkillCode = skillCode,
                ActorId = actorId,
                TargetIdList = targetIdList,
                ActualTargetIdList = actualTargetIdList
            };

            return ailment;
        }

        public static BattleEventValueObject CreateBuff(
            BuffCode buffCode,
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            float rate,
            int turn,
            LifetimeCode lifetimeCode)
        {
            var buff = new BattleEventValueObject
            {
                BattleEventCode = BattleEventCode.Skill,
                BuffCode = buffCode,
                SkillCode = skillCode,
                ActorId = actorId,
                TargetIdList = targetIdList,
                ActualTargetIdList = targetIdList,
                Rate = rate,
                Turn = turn,
                LifetimeCode = lifetimeCode
            };

            return buff;
        }

        public static BattleEventValueObject CreateCure(
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CuringValueObject> curingList)
        {
            var targetIdList = curingList
                .Select(x => x.TargetId)
                .ToList();

            var cure = new BattleEventValueObject
            {
                ActorId = actorId,
                SkillCode = skillCode,
                TargetIdList = targetIdList,
                ActualTargetIdList = targetIdList,
                CuringList = curingList
            };

            return cure;
        }

        public static BattleEventValueObject CreateDamage(
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<AttackValueObject> attackList)
        {
            var targetIdList = attackList
                .Select(x => x.TargetId)
                .Distinct()
                .ToList();
            
            var damage = new BattleEventValueObject
            {
                BattleEventCode = BattleEventCode.Skill,
                SkillCode = skillCode,
                ActorId = actorId,
                TargetIdList = targetIdList,
                ActualTargetIdList = targetIdList,
                AttackList = attackList
            };

            return damage;
        }

        public static BattleEventValueObject CreateDestroy(
            BodyPartCode bodyPartCode,
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList,
            int destroyCount)
        {
            var destroy = new BattleEventValueObject
            {
                BattleEventCode = BattleEventCode.Skill,
                BodyPartCode = bodyPartCode,
                SkillCode = skillCode,
                ActorId = actorId,
                TargetIdList = targetIdList,
                ActualTargetIdList = actualTargetIdList,
                DestroyCount = destroyCount
            };

            return destroy;
        }

        public static BattleEventValueObject CreateEnhance(
            EnhanceCode enhanceCode,
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            int turn,
            LifetimeCode lifetimeCode)
        {
            var enhance = new BattleEventValueObject
            {
                BattleEventCode = BattleEventCode.Skill,
                EnhanceCode = enhanceCode,
                SkillCode = skillCode,
                ActorId = actorId,
                TargetIdList = targetIdList,
                ActualTargetIdList = targetIdList,
                Turn = turn,
                LifetimeCode = lifetimeCode
            };

            return enhance;
        }

        public static BattleEventValueObject CreateReset(
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<AilmentCode>? ailmentCodeList = null,
            IReadOnlyList<SlipCode>? slipCodeList = null,
            IReadOnlyList<BodyPartCode>? bodyPartCodeList = null)
        {
            var reset = new BattleEventValueObject
            {
                BattleEventCode = BattleEventCode.Skill,
                SkillCode = skillCode,
                ActorId = actorId,
                TargetIdList = targetIdList,
                ResetAilmentCodeList = ailmentCodeList ?? Array.Empty<AilmentCode>(),
                ResetSlipCodeList = slipCodeList ?? Array.Empty<SlipCode>(),
                ResetBodyPartCodeList = bodyPartCodeList ?? Array.Empty<BodyPartCode>()
            };

            return reset;
        }
        
        public static BattleEventValueObject CreateRestore(
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            int technicalPoint)
        {
            var restore = new BattleEventValueObject
            {
                BattleEventCode = BattleEventCode.Skill,
                SkillCode = skillCode,
                ActorId = actorId,
                TargetIdList = targetIdList,
                ActualTargetIdList = targetIdList,
                TechnicalPoint = technicalPoint
            };

            return restore;
        }
        
        public static BattleEventValueObject CreateSlip(
            SlipCode slipCode,
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList)
        {
            var slip = new BattleEventValueObject
            {
                BattleEventCode = BattleEventCode.Skill,
                SlipCode = slipCode,
                SkillCode = skillCode,
                ActorId = actorId,
                TargetIdList = targetIdList,
                ActualTargetIdList = actualTargetIdList
            };

            return slip;
        }
        
        public static BattleEventValueObject CreateSlipDamage(
            SlipCode slipCode,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<AttackValueObject> attackList)
        {
            var slipDamage = new BattleEventValueObject
            {
                BattleEventCode = BattleEventCode.SlipDamage,
                SlipCode = slipCode,
                TargetIdList = targetIdList,
                ActualTargetIdList = targetIdList,
                AttackList = attackList,
            };

            return slipDamage;
        }
    }
}