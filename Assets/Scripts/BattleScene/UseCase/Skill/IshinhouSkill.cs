using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    ///     医心方
    /// </summary>
    internal class IshinhouSkill : AbstractSkill
    {
        private readonly IshinhouSkillElement _ishinhouSkillElement;

        public override int GetTechnicalPoint()
        {
            return 3;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Katana;
        }

        public override Range GetRange()
        {
            return Range.Oneself;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.IshinhouDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.RemoveAilmentsMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_ishinhouSkillElement);
        }
    }
}