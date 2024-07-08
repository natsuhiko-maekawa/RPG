using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     スターシェル
    /// </summary>
    internal class StarShellSkill : AbstractSkill
    {
        public StarShellSkill(StarShellSkillElement starShellSkillElement)
        {
            BuffSkillElementList = ImmutableList.Create<BuffSkillElement>(starShellSkillElement);
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