using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     火炎放射
    /// </summary>
    internal class FlameThrowSkill : AbstractSkill
    {
        private readonly BasicDamageSkillElement _basicDamageSkillElement;
        private readonly BurningSkillElement _burningSkillElement;

        public FlameThrowSkill(BasicDamageSkillElement basicDamageSkillElement, BurningSkillElement burningSkillElement)
        {
            DamageSkillElementList = ImmutableList.Create<DamageSkillElement>(basicDamageSkillElement);
            SlipDamageElementList = ImmutableList.Create<SlipDamageElement>(burningSkillElement);
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