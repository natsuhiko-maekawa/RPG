using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     火炎放射
    /// </summary>
    internal class FlameThrowSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.FlameThrow;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.FrameThrowMessage;

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new BasicDamage());

        public override ImmutableList<AbstractSlipDamage> SlipDamageList { get; }
            = ImmutableList.Create<AbstractSlipDamage>(new BleedingSkill());
    }
}