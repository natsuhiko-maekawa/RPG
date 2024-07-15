using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;
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
            BuffList = ImmutableList.Create<AbstractBuff>(afterimageSkill);
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