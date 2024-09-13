using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class DamageValueObject
    {
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }
        public ImmutableList<AttackValueObject> AttackList { get; }
        
        public DamageValueObject(
            CharacterId actorId,
            SkillCode skillCode,
            ImmutableList<AttackValueObject> attackList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            TargetIdList = attackList
                .Select(x => x.TargetId)
                .ToImmutableList();
            AttackList = attackList;
        }

        public ImmutableDictionary<CharacterId, int> GetDamageDictionary()
        {
            var damageDictionary = AttackList
                .Where(x => x.IsHit)
                .GroupBy(x => x.TargetId)
                .Select(x => x
                    .Select(y => (targetId: y.TargetId, amount: y.Amount))
                    .Aggregate((y, z) => (y.targetId, y.amount + z.amount)))
                .ToImmutableDictionary(x => x.targetId, x => x.amount);
            return damageDictionary;
        }
    }
}