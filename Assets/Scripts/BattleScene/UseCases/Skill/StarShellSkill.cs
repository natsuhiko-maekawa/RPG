using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     スターシェル
    /// </summary>
    internal class StarShellSkill : AbstractSkill
    {
        public StarShellSkill(StarShell starShell)
        {
            BuffList = ImmutableList.Create<AbstractBuff>(starShell);
        }

        public override int GetTechnicalPoint()
        {
            return 3;
        }

        public override ImmutableList<BodyPartCode> GetDependencyList()
        {
            return ImmutableList.Create(BodyPartCode.Arm);
        }

        public MessageCode GetMsg()
        {
            return MessageCode.BuffMessage;
        }

        public override Range GetRange()
        {
            return Range.Oneself;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Gun;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.StarShellDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.BuffMessage;
        }
    }
}