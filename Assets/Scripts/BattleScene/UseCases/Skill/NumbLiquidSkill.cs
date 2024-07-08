using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     痺れる粘液
    /// </summary>
    internal class NumbLiquidSkill : AbstractSkill
    {
        public NumbLiquidSkill(
            BasicDamageSkillElement basicDamageSkillElement,
            ParalysisSkillElement paralysisSkillElement)
        {
            DamageSkillElementList = ImmutableList.Create<DamageSkillElement>(basicDamageSkillElement);
            AilmentSkillElementList = ImmutableList.Create<AilmentSkillElement>(paralysisSkillElement);
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
            return MessageCode.NumbLiquidMessage;
        }
    }
}