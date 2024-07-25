using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.ValueObject
{
    public class DamageSkillResultValueObject : ISkillResult, IDamageResult
    {
        public DamageSkillResultValueObject(
            CharacterId actorId,
            SkillCode skillCode,
            ImmutableList<DamageResultValueObject> damageList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            TargetIdList = damageList
                .Select(x => x.TargetId)
                .ToImmutableList();
            DamageList = damageList;
        }

        public ImmutableList<DamageResultValueObject> DamageList { get; }

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