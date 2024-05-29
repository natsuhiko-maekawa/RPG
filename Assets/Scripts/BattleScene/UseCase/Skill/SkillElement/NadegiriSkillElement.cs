using BattleScene.Domain.DomainService;
using BattleScene.UseCase.Skill.AbstractClass;
using BattleScene.UseCase.Skill.Expression;

namespace BattleScene.UseCase.Skill.SkillElement
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