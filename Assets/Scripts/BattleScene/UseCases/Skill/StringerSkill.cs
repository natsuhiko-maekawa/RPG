using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
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
            BasicDamageSkillElement basicDamageSkillElement,
            PoisoningSkillElement poisoningSkillElement)
        {
            DamageSkillElementList = ImmutableList.Create<DamageSkillElement>(basicDamageSkillElement);
            SlipDamageElementList = ImmutableList.Create<SlipDamageElement>(poisoningSkillElement);
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