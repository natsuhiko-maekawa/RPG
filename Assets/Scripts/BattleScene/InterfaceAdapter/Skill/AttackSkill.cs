using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
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