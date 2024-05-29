using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    /// 力溜め
    /// </summary>
    internal class MusterStrengthSkill : AbstractSkill
    {
        private readonly MusterStrengthSkillElement _musterStrengthSkillElement;
        
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

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_musterStrengthSkillElement);
        }
    }
}