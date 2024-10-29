using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    public class BleedingSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Bleeding;
        public override Range Range { get; } = Range.Player;
        public override MessageCode AttackMessageCode { get; } = MessageCode.BleedingMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new SlipDamage() };
    }
}