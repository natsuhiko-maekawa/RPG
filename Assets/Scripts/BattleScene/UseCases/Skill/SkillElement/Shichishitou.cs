using BattleScene.Domain.DomainService;
using BattleScene.Domain.Expression;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class Shichishitou : AbstractDamage
    {
        public Shichishitou(
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
            return 7;
        }

        public override float GetDamageRate()
        {
            return 1.0f / 7.0f;
        }
    }
}