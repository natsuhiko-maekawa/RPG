using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Skill.SkillElement;

namespace BattleScene.UseCases.Skill
{
    internal class AttackSkill : AbstractSkill
    {
        public AttackSkill(BasicDamage basicDamage)
        {
            DamageList = ImmutableList.Create<AbstractDamage>(basicDamage);
        }

        public override Range GetRange()
        {
            return Range.Solo;
        }

        public override MessageCode GetAttackMessage()
        {
            return MessageCode.AttackMessage;
        }
    }
}