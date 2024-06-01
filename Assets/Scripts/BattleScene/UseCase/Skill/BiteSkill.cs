using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;
using Utility;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    ///     噛みつき
    /// </summary>
    internal class BiteSkill : AbstractSkill
    {
        private readonly BasicDamageSkillElement _basicDamageSkillElement;
        private readonly IRandomEx _randomEx;

        public BiteSkill(BasicDamageSkillElement basicDamageSkillElement, IRandomEx randomEx)
        {
            _basicDamageSkillElement = basicDamageSkillElement;
            _randomEx = randomEx;
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

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_basicDamageSkillElement);
        }
    }
}