using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.UseCases.Service.Order;

namespace BattleScene.UseCases.UseCase
{
    public class OrderUseCase
    {
        private readonly ActionTimeService _actionTime;
        private readonly OrderService _order;
        private readonly OrderedItemsDomainService _orderedItems;

        public OrderUseCase(
            ActionTimeService actionTime,
            OrderService order,
            OrderedItemsDomainService orderedItems)
        {
            _actionTime = actionTime;
            _order = order;
            _orderedItems = orderedItems;
        }

        public void Register()
        {
            _order.Update();
            _actionTime.Update();
        }

        public (CharacterId?, AilmentCode, SlipCode) First()
        {
            var orderedItem = _orderedItems.First();
            orderedItem.TryGetCharacterId(out var actorId);
            orderedItem.TryGetAilmentCode(out var ailmentCode);
            orderedItem.TryGetSlipDamageCode(out var slipDamageCode);
            return (actorId, ailmentCode, slipDamageCode);
        }
    }
}