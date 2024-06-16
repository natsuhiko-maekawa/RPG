using BattleScene.Domain.DomainService;
using BattleScene.UseCase.Skill.Expression;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class RandomShotSkillElement : DamageSkillElement
    {
        public RandomShotSkillElement(
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
            return 10;
        }

        public override float GetDamageRate()
        {
            return 0.5f;
        }

        public override float GetHitRate()
        {
            return 0.5f;
        }
    }
}