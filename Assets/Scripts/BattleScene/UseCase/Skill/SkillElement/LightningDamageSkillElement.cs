using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.UseCase.Skill.AbstractClass;
using BattleScene.UseCase.Skill.Expression;

namespace BattleScene.UseCase.Skill.SkillElement
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