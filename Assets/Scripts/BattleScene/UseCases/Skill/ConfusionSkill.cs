using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;
using static BattleScene.Domain.Code.Range;

namespace BattleScene.UseCases.Skill
{
    /// <summary>
    ///     混乱
    /// </summary>
    internal class ConfusionSkill : AbstractSkill
    {
        public ConfusionSkill(AlwaysHitDamage alwaysHitDamage)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(alwaysHitDamage);
        }

        public override Range GetRange()
        {
            return Oneself;
        }

        public override PlayerImageCode GetPlayerImageCode()
        {
            return PlayerImageCode.Confusion;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.ConfusionActMessage;
        }
    }
}