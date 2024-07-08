using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.AbstractClass;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     火炎放射
    /// </summary>
    internal class FlameThrowSkill : AbstractSkill
    {
        private readonly BasicDamage _basicDamage;
        private readonly BurningSkill _burningSkill;

        public FlameThrowSkill(BasicDamage basicDamage, BurningSkill burningSkill)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(basicDamage);
            SlipDamageList = ImmutableList.Create<AbstractSlipDamage>(burningSkill);
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Damaged;
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.FrameThrowMessage;
        }
    }
}