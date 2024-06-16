using BattleScene.Domain.DomainService;
using BattleScene.UseCase.Skill.Expression;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class FiveTimeDamageSkillElement : DamageSkillElement
    {
        public FiveTimeDamageSkillElement(
            AttacksWeakPointEvaluation attacksWeakPointEvaluation,
            DamageExpression damageExpression,
            HitEvaluation hitEvaluation,
            OrderedItemsDomainService orderedItems)
            : base(
                attacksWeakPointEvaluation,
                damageExpression,
                hitEvaluation,
                orderedItems)
        {
        }

        public override int GetAttackNum()
        {
            return 5;
        }

        public override float GetDamageRate()
        {
            return 1.0f / 5.0f;
        }
    }
}