using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.AbstractClass;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     力溜め
    /// </summary>
    internal class MusterStrengthSkill : AbstractSkill
    {
        public MusterStrengthSkill(MusterStrength musterStrength)
        {
            BuffList = ImmutableList.Create<AbstractBuff>(musterStrength);
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