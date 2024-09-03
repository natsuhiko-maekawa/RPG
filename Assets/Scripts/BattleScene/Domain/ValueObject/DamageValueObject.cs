using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class DamageValueObject
    {
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

        public ImmutableList<AttackValueObject> AttackList { get; }

        public int GetTotal()
        {
            return AttackList
                .Sum(x => x.IsHit ? x.Amount : 0);
        }

        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }

        public bool Success()
        {
            return AttackList.All(x => !x.IsHit);
        }

        public int HitCount()
        {
            return AttackList
                .Count(x => x.IsHit);
        }
    }
}