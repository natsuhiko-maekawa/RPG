using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

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
            DamageList = ImmutableList.Create<AbstractDamage>(basicDamage);
            SlipDamageList = ImmutableList.Create<AbstractSlipDamage>(poisoningSkill);
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