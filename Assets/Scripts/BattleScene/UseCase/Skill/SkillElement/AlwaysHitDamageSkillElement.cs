using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.UseCase.Skill.AbstractClass;
using BattleScene.UseCase.Skill.Expression;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class AlwaysHitDamageSkillElement : DamageSkillElement
    {
        public AlwaysHitDamageSkillElement(
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

        public override bool IsHit(CharacterId _)
        {
            return true;
        }
    }
}