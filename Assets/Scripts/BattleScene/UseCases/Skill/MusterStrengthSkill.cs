using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     力溜め
    /// </summary>
    internal class MusterStrengthSkill : AbstractSkill
    {
        public MusterStrengthSkill(MusterStrengthSkillElement musterStrengthSkillElement)
        {
            BuffSkillElementList = ImmutableList.Create<BuffSkillElement>(musterStrengthSkillElement);
        }

        public override int GetTechnicalPoint()
        {
            return 3;
        }

        public override Range GetRange()
        {
            return Range.Oneself;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Katana;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.MusterStrengthDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.BuffMessage;
        }
    }
}