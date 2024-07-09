using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Aggregate
{
    public class SkillAggregate : BaseEntity<SkillAggregate, SkillId>
    {
        public override SkillId Id { get; }
        public CharacterId CharacterId { get; }
        public SkillCode SkillCode { get; }
        public ISkill Skill { get; }

        public SkillAggregate(
            CharacterId characterId,
            SkillCode skillCode,
            ISkill skill)
        {
            Id = new SkillId((characterId, skillCode));
            CharacterId = characterId;
            SkillCode = skillCode;
            Skill = skill;
        }
    }
}