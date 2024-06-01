using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    ///     スターシェル
    /// </summary>
    internal class StarShellSkill : AbstractSkill
    {
        private readonly StarShellSkillElement _starShellSkillElement;

        public StarShellSkill(StarShellSkillElement starShellSkillElement)
        {
            _starShellSkillElement = starShellSkillElement;
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

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_starShellSkillElement);
        }
    }
}