using System.Collections.Immutable;
using BattleScene.Domain.AbstractClass;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;
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
            DamageSkillElementList = ImmutableList.Create<AbstractDamage>(alwaysHitDamage);
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