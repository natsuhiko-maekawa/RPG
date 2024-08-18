using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class SkillEntity : BaseEntity<SkillEntity, CharacterId>
    {
        public SkillEntity(
            CharacterId id,
            SkillValueObject skill)
        {
            Id = id;
            Skill = skill;
        }

        public override CharacterId Id { get; }
        [Obsolete]
        public SkillCode SkillCode { get; }
        public SkillValueObject Skill { get; }
        
        public ImmutableList<AilmentValueObject> AilmentList { get; private set; }
        public ImmutableList<BuffValueObject> BuffList { get; private set; }
        public ImmutableList<DamageValueObject> DamageList { get; }
        
        public bool TryRemoveFirstEffect()
        {
            if (!AilmentList.IsEmpty)
            {
                AilmentList = AilmentList.RemoveAt(0);
                return true;
            }

            if (!BuffList.IsEmpty)
            {
                BuffList = BuffList.RemoveAt(0);
                return true;
            }

            throw new NotImplementedException();
            return false;
        }
    }
}