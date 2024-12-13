using BattleScene.Domain.Codes;
using BattleScene.Domain.DomainServices;
using BattleScene.Domain.Entities;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.Service.Order;

namespace BattleScene.UseCases.UseCase
{
    public class OrderUseCase
    {
        private readonly ActionTimeService _actionTime;
        private readonly BattleLoggerService _battleLogger;
        private readonly OrderService _order;
        private readonly OrderedItemsDomainService _orderedItems;

        public OrderUseCase(
            ActionTimeService actionTime,
            BattleLoggerService battleLogger,
            OrderService order,
            OrderedItemsDomainService orderedItems)
        {
            _actionTime = actionTime;
            _battleLogger = battleLogger;
            _order = order;
            _orderedItems = orderedItems;
        }

        public void Register()
        {
            _order.Update();
            _actionTime.Update();
            _battleLogger.Log(First());
        }

        public (CharacterEntity?, AilmentCode, SlipCode) First()
        {
            var orderedItem = _orderedItems.First();
            orderedItem.TryGetActor(out var actor);
            orderedItem.TryGetAilmentCode(out var ailmentCode);
            orderedItem.TryGetSlipCode(out var slipDamageCode);
            return (actor, ailmentCode, slipDamageCode);
        }
    }
}