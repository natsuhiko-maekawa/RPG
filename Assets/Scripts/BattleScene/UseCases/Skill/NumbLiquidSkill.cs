using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     痺れる粘液
    /// </summary>
    internal class NumbLiquidSkill : AbstractSkill
    {
        private readonly BasicDamageSkillElement _basicDamageSkillElement;
        private readonly ParalysisSkillElement _paralysisSkillElement;

        public NumbLiquidSkill(
            BasicDamageSkillElement basicDamageSkillElement,
            ParalysisSkillElement paralysisSkillElement)
        {
            _basicDamageSkillElement = basicDamageSkillElement;
            _paralysisSkillElement = paralysisSkillElement;
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

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_basicDamageSkillElement, _paralysisSkillElement);
        }
    }
}