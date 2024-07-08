using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.AbstractClass;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     毒針
    /// </summary>
    internal class StringerSkill : AbstractSkill
    {
        public StringerSkill(
            BasicDamage basicDamage,
            PoisoningSkill poisoningSkill)
        {
            DamageSkillElementList = ImmutableList.Create<AbstractDamage>(basicDamage);
            SlipDamageElementList = ImmutableList.Create<AbstractSlipDamage>(poisoningSkill);
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Damaged;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.StringerMessage;
        }
    }
}