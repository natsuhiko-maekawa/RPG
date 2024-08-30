using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
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
    }
}