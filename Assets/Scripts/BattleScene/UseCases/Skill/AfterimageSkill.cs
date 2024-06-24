using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement;
using static BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     残像
    /// </summary>
    internal class AfterimageSkill : AbstractSkill
    {
        private readonly AfterImageSkillElement _afterimageSkillSkillElement;

        public AfterimageSkill(AfterImageSkillElement afterimageSkillSkillElement)
        {
            _afterimageSkillSkillElement = afterimageSkillSkillElement;
        }

        public override Range GetRange()
        {
            return Oneself;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.AfterimageMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_afterimageSkillSkillElement);
        }
    }
}