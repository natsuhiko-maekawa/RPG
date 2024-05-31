using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement;

namespace BattleScene.UseCase.Skill
{
    /// <summary>
    ///     口寄せ
    /// </summary>
    internal class KuchiyoseSkill : AbstractSkill
    {
        private readonly ConfusionSkillElement _confusionSkillElement;

        public override int GetTechnicalPoint()
        {
            return 10;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Katana;
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.KuchiyoseDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.NoMessage;
        }

        public override ImmutableList<ISkillElement> GetSkillService()
        {
            return ImmutableList.Create<ISkillElement>(_confusionSkillElement);
        }
    }
}