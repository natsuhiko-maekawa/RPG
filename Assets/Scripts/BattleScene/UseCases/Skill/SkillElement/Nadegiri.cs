using BattleScene.Domain.DomainService;
using BattleScene.Domain.Expression;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class Nadegiri : AbstractDamage
    {
        public Nadegiri(
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

        public override float GetDamageRate()
        {
            return 2.0f / 3.0f;
        }
    }
}