using BattleScene.Domain.DomainService;
using BattleScene.UseCase.Skill.AbstractClass;
using BattleScene.UseCase.Skill.Expression;

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