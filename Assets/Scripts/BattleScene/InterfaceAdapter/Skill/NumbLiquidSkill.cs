using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     痺れる粘液
    /// </summary>
    internal class NumbLiquidSkill : AbstractSkill
    {
        public NumbLiquidSkill(
            BasicDamage basicDamage,
            Paralysis paralysis)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(basicDamage);
            AilmentList = ImmutableList.Create<AbstractAilment>(paralysis);
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Damaged;
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.NumbLiquidMessage;
        }
    }
}