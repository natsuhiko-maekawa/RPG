using System;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    [Obsolete]
    public class OrderDecision
    {
        private readonly ActionTimeService _actionTime;
        private readonly OrderService _order;

        public OrderDecision(
            ActionTimeService actionTime,
            OrderService order)
        {
            _actionTime = actionTime;
            _order = order;
        }

        public void Execute()
        {
            _order.Update();
            _actionTime.Update();
        }
    }
}