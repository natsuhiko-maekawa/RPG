using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     火炎放射
    /// </summary>
    public class FlameThrowSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.FlameThrow;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.FrameThrowMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };

        public override IReadOnlyList<BaseSlip> SlipDamageList { get; }
            = new[] { new BleedingSkill() };
    }
}