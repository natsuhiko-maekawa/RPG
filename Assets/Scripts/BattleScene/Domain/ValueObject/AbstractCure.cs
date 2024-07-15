using BattleScene.Domain.DomainService;
using BattleScene.Domain.Expression;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public abstract class AbstractCure
    {
        private readonly CureExpression _cureExpression;
        private readonly OrderedItemsDomainService _orderedItems;

        protected AbstractCure(
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