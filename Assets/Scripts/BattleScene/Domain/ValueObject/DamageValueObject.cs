using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.ValueObject
{
    public class DamageValueObject : ISkillResult, IDamageResult
    {
        public DamageValueObject(
            CharacterId actorId,
            SkillCode skillCode,
            ImmutableList<AttackValueObject> damageList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            TargetIdList = damageList
                .Select(x => x.TargetId)
                .ToImmutableList();
            DamageList = damageList;
        }

        public ImmutableList<AttackValueObject> DamageList { get; }

        public int GetTotal()
        {
            return DamageList
                .Sum(x => x.IsHit ? x.Amount : 0);
        }

        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }

        public bool Success()
        {
            return DamageList.All(x => !x.IsHit);
        }

        public int HitCount()
        {
            return DamageList
                .Count(x => x.IsHit);
        }
    }
}