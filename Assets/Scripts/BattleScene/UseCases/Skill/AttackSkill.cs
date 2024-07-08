using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.AbstractClass;
using BattleScene.UseCases.Skill.SkillElement;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

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