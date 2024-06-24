using BattleScene.Domain.DomainService;
using BattleScene.UseCases.Skill.Expression;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class NadegiriSkillElement : DamageSkillElement
    {
        public NadegiriSkillElement(
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