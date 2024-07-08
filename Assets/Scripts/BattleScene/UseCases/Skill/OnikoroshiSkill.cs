using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     鬼殺し
    /// </summary>
    internal class OnikoroshiSkill : AbstractSkill
    {
        public OnikoroshiSkill(ConfusionSkillElement confusionSkillElement)
        {
            AilmentSkillElementList = ImmutableList.Create<AilmentSkillElement>(confusionSkillElement);
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
    }
}