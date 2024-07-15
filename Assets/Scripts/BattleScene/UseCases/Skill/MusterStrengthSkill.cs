using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

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