using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class DamageSkillResultValueObject : ISkillResult, IDamageResult
    {
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }
        public ImmutableList<DamageValueObject> DamageList { get; }

        public DamageSkillResultValueObject(
            CharacterId actorId, 
            SkillCode skillCode, 
            ImmutableList<DamageValueObject> damageList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            TargetIdList = damageList
                .Select(x => x.TargetId)
                .ToImmutableList();
            DamageList = damageList;
        }

        public bool Success()
        {
            return DamageList.All(x => !x.IsHit);
        }
        
        public int GetTotal()
        {
            return DamageList
                .Sum(x => x.IsHit ? x.Amount : 0);
        }
        
        public int HitCount()
        {
            return DamageList
                .Count(x => x.IsHit);
        }
    }
}