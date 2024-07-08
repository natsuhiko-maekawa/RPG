using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
using static BattleScene.Domain.Code.MessageCode;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     粘液
    /// </summary>
    internal class LiquidSkill : AbstractSkill
    {
        public LiquidSkill(
            BasicDamageSkillElement basicDamageSkillElement,
            SuffocationSkillElement suffocationSkillElement)
        {
            DamageSkillElementList = ImmutableList.Create<DamageSkillElement>(basicDamageSkillElement);
            SlipDamageElementList = ImmutableList.Create<SlipDamageElement>(suffocationSkillElement);
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Suffocation;
        }

        public override MessageCode GetAttackMessage()
        {
            return InvasiveMouthMessage;
        }
    }
}