using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.AbstractClass;
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
            BasicDamage basicDamage,
            SuffocationSkill suffocationSkill)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(basicDamage);
            SlipDamageList = ImmutableList.Create<AbstractSlipDamage>(suffocationSkill);
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