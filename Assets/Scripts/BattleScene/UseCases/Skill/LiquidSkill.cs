using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;
using static BattleScene.Domain.Code.MessageCode;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     粘液
    /// </summary>
    internal class LiquidSkill : AbstractSkill
    {
        private readonly BasicDamageSkillElement _basicDamageSkillElement;
        private readonly SuffocationSkillElement _suffocationSkillElement;

        public LiquidSkill(
            BasicDamageSkillElement basicDamageSkillElement,
            SuffocationSkillElement suffocationSkillElement)
        {
            _basicDamageSkillElement = basicDamageSkillElement;
            _suffocationSkillElement = suffocationSkillElement;
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

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_basicDamageSkillElement, _suffocationSkillElement);
        }
    }
}