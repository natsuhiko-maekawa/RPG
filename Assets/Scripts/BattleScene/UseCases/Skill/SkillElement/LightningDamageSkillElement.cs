using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.Skill.Expression;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class LightningDamageSkillElement : DamageSkillElement
    {
        public LightningDamageSkillElement(
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

        public override ImmutableList<MatAttrCode> GetMatAttrCode()
        {
            return ImmutableList.Create(MatAttrCode.Lightning);
        }
    }
}