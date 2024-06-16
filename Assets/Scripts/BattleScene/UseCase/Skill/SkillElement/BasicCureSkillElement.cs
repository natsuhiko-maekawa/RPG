using BattleScene.Domain.DomainService;
using BattleScene.UseCase.Skill.Expression;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    internal class BasicCureSkillElement : CureSkillElement
    {
        public BasicCureSkillElement(
            CureExpression cureExpression,
            OrderedItemsDomainService orderedItems)
            : base(
                cureExpression,
                orderedItems)
        {
        }
    }
}