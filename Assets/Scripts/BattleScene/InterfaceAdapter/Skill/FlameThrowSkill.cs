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
    internal class FlameThrowSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.FlameThrow;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.FrameThrowMessage;

        public override ImmutableList<BaseDamage> DamageList { get; }
            = ImmutableList.Create<BaseDamage>(new BasicDamage());

        public override ImmutableList<BaseSlip> SlipDamageList { get; }
            = ImmutableList.Create<BaseSlip>(new BleedingSkill());
    }
}