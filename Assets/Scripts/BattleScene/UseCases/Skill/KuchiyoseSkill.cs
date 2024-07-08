using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     口寄せ
    /// </summary>
    internal class KuchiyoseSkill : AbstractSkill
    {
        public KuchiyoseSkill(Confusion confusion)
        {
            AilmentSkillElementList = ImmutableList.Create<AbstractAilment>(confusion);
        }

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
    }
}