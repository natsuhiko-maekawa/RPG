using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public abstract class PrimeSkillValueObject
    {
        public ActionCode ActionCode { get; protected init; }
        public AilmentCode AilmentCode { get; protected init; }
        public BodyPartCode BodyPartCode { get; protected init; }
        public BuffCode BuffCode { get; protected init; }
        public SlipDamageCode SlipDamageCode { get; protected init; }
        public SkillCode SkillCode { get; protected init; }
        public CharacterId ActorId { get; protected init; }
        public IReadOnlyList<CharacterId> TargetIdList { get; protected init; }
        public IReadOnlyList<CharacterId> ActualTargetIdList { get; protected init; }
        public bool IsFailure => ActualTargetIdList.Count == 0;
        public float Rate { get; protected init; }
        public int Turn { get; protected init; }
        public LifetimeCode LifetimeCode { get; protected init; }
        public int DestroyCount { get; protected init; }
        public IReadOnlyList<AttackValueObject> AttackList { get; protected init; }
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
        public int TechnicalPoint { get; protected init; }
    }
}