using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
using Utility.Interface;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     噛みつき
    /// </summary>
    internal class BiteSkill : AbstractSkill
    {
        private readonly IRandomEx _randomEx;

        public BiteSkill(BasicDamageSkillElement basicDamageSkillElement, IRandomEx randomEx)
        {
            _randomEx = randomEx;
            DamageSkillElementList = ImmutableList.Create<DamageSkillElement>(basicDamageSkillElement);
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
            return _randomEx.Choice(new[] { BiteArmMessage, BiteLegMessage, BiteStomachMessage });
        }
    }
}