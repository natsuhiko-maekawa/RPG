using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    public class SuffocationSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Suffocation;
        public override Range Range { get; } = Range.Player;
        public override MessageCode AttackMessageCode { get; } = MessageCode.SuffocationMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new [] { new SlipDamage() };
    }
}