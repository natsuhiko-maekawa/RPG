using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     鬼殺し
    /// </summary>
    internal class OnikoroshiSkill : AbstractSkill
    {
        private readonly ConfusionSkillElement _confusionSkillElement;

        public OnikoroshiSkill(ConfusionSkillElement confusionSkillElement)
        {
            _confusionSkillElement = confusionSkillElement;
        }

        public override ImmutableList<BodyPartCode> GetDependencyList()
        {
            return ImmutableList.Create(BodyPartCode.Arm);
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
            return MessageCode.OnikoroshiMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_confusionSkillElement);
        }
    }
}