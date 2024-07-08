using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.AbstractClass;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
using static BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     残像
    /// </summary>
    internal class AfterimageSkill : AbstractSkill
    {
        public AfterimageSkill(AfterImage afterimageSkill)
        {
            BuffSkillElementList = ImmutableList.Create<AbstractBuff>(afterimageSkill);
        }

        public override Range GetRange()
        {
            return Oneself;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.AfterimageMessage;
        }
    }
}