using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     村雨
    /// </summary>
    internal class MurasameSkill : AbstractSkill
    {
        public MurasameSkill(
            BasicDamageSkillElement basicDamageSkillElement,
            BurningResetSkillElement burningResetSkillElement)
        {
            DamageSkillElementList = ImmutableList.Create<DamageSkillElement>(basicDamageSkillElement);
            ResetSkillElementList = ImmutableList.Create<ResetSkillElement>(burningResetSkillElement);
        }

        public override int GetTechnicalPoint()
        {
            return 5;
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Katana;
        }

        public override MessageCode GetDescription()
        {
            return MessageCode.MurasameDescription;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.DamageMessage;
        }
    }
}