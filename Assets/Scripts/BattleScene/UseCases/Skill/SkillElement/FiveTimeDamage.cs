using BattleScene.Domain.DomainService;
using BattleScene.Domain.Expression;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class FiveTimeDamage : AbstractDamage
    {
        public FiveTimeDamage(
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