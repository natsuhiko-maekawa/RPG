using BattleScene.Domain.Codes;
using BattleScene.Domain.DomainServices;
using BattleScene.Domain.Entities;
using BattleScene.UseCases.Services;
using BattleScene.UseCases.Services.Order;

namespace BattleScene.UseCases.UseCases
{
    public class OrderUseCase
    {
        private readonly ActionTimeService _actionTime;
        private readonly BattleLoggerService _battleLogger;
        private readonly OrderService _order;
        private readonly OrderItemsDomainService _orderItems;

        public OrderUseCase(
            ActionTimeService actionTime,
            BattleLoggerService battleLogger,
            OrderService order,
            OrderItemsDomainService orderItems)
        {
            _actionTime = actionTime;
            _battleLogger = battleLogger;
            _order = order;
            _orderItems = orderItems;
        }

        public void Register()
        {
            _order.Update();
            _actionTime.Update();
            _battleLogger.Log(First());
        }

        public (CharacterEntity?, AilmentCode, SlipCode) First()
        {
            var orderItem = _orderItems.First();
            orderItem.TryGetActor(out var actor);
            orderItem.TryGetAilmentCode(out var ailmentCode);
            orderItem.TryGetSlipCode(out var slipDamageCode);
            return (actor, ailmentCode, slipDamageCode);
        }
    }
}