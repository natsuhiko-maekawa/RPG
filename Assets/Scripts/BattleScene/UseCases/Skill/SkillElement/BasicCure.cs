using BattleScene.Domain.DomainService;
using BattleScene.Domain.Expression;
using BattleScene.Domain.ValueObject;

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