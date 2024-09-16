using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.UseCases.Output
{
    [Obsolete]
    public class OrderView
    {
        private readonly IRepository<OrderedItemEntity, OrderId> _orderedItemRepository;
        private readonly IOrderViewPresenter _orderView;

        public OrderView(
            IRepository<OrderedItemEntity, OrderId> orderedItemRepository,
            IOrderViewPresenter orderView)
        {
            _orderedItemRepository = orderedItemRepository;
            _orderView = orderView;
        }

        public void Out()
        {
            var order = _orderedItemRepository.Select()
                .OrderBy(x => x.OrderNumber)
                .ToImmutableList();
            _orderView.Start(order);
        }
    }
}