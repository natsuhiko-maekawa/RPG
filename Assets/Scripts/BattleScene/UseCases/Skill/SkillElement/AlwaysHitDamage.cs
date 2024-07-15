using BattleScene.Domain.DomainService;
using BattleScene.Domain.Expression;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class AlwaysHitDamage : AbstractDamage
    {
        public AlwaysHitDamage(
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