using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    public class PoisoningSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Poisoning;
        public override Range Range { get; } = Range.Player;
        public override MessageCode AttackMessageCode { get; } = MessageCode.PoisoningMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new SlipDamage() };
    }
}