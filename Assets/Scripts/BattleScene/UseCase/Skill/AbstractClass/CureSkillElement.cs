using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.Expression;

namespace BattleScene.UseCase.Skill.AbstractClass
{
    internal abstract class CureSkillElement : ISkillElement
    {
        private readonly CureExpression _cureExpression;
        private readonly OrderedItemsDomainService _orderedItems;

        protected CureSkillElement(
            CureExpression cureExpression,
            OrderedItemsDomainService orderedItems)
        {
            _cureExpression = cureExpression;
            _orderedItems = orderedItems;
        }

        public virtual float GetCureRate()
        {
            return 1.0f;
        }

        public virtual int GetCureAmount(CharacterId targetId)
        {
            var actorId = _orderedItems.FirstCharacterId();
            return _cureExpression.Evaluate(actorId, targetId, this);
        }
    }
}