using BattleScene.Domain.DomainService;
using BattleScene.UseCases.Skill.Expression;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
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