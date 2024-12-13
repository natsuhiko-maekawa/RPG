using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;

namespace BattleScene.Presenters.Skills
{
    public class AttackSkill : BaseSkill
    {
        public AttackSkill(BasicDamage basicDamage)
        {
            DamageList = new[] { basicDamage };
        }

        public override SkillCode SkillCode { get; } = SkillCode.Attack;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; } =
            new[] { new BasicDamage() };
    }
}