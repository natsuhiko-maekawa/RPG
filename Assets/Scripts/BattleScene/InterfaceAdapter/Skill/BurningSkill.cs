using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    public class BurningSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Burning;
        public override Range Range { get; } = Range.Player;
        public override MessageCode AttackMessageCode { get; } = MessageCode.BurningMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new SlipDamage() };
    }
}