using BattleScene.Domain.DomainService;
using BattleScene.UseCases.Skill.Expression;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class BasicCure : AbstractCure
    {
        public BasicCure(
            CureExpression cureExpression,
            OrderedItemsDomainService orderedItems)
            : base(
                cureExpression,
                orderedItems)
        {
        }
    }
}