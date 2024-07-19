using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     村雨
    /// </summary>
    internal class MurasameSkill : AbstractSkill
    {
        public MurasameSkill(
            BasicDamage basicDamage,
            BurningReset burningReset)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(basicDamage);
            ResetList = ImmutableList.Create<AbstractReset>(burningReset);
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