using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;
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
            SkillEffectList = skill.AilmentList
                .Cast<ISkillEffect>()
                .Concat(skill.BuffList)
                .Concat(skill.DamageList)
                .ToImmutableList();
        }

        public override CharacterId Id { get; }
        [Obsolete]
        public SkillCode SkillCode { get; }
        public SkillValueObject Skill { get; }

        public ImmutableList<ISkillEffect> SkillEffectList { get; private set; }
        
        public ISkillEffect DequeueSkillEffect()
        {
            var skillEffect = SkillEffectList.First();
            SkillEffectList = SkillEffectList.RemoveAt(0);
            return skillEffect;
        }
    }
}