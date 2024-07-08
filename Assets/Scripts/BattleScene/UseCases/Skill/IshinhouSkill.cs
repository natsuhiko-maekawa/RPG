using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     医心方
    /// </summary>
    internal class IshinhouSkill : AbstractSkill
    {
        public IshinhouSkill(Ishinhou ishinhou)
        {
            ResetSkillElementList = ImmutableList.Create<AbstractReset>(ishinhou);
        }

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
    }
}