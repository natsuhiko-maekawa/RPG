using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Output.Interface;
using BattleScene.UseCases.View.OrderView.OutputBoundary;

namespace BattleScene.UseCases.Output
{
    public class OrderView : IOutput
    {
        private readonly IRepository<OrderedItemEntity, OrderNumber> _orderedItemRepository;
        private readonly IOrderViewPresenter _orderView;

        public OrderView(
            IRepository<OrderedItemEntity, OrderNumber> orderedItemRepository,
            IOrderViewPresenter orderView)
        {
            _orderedItemRepository = orderedItemRepository;
            _orderView = orderView;
        }

        public void Out()
        {
            var order = _orderedItemRepository.Select();
            _orderView.Start(order);
        }
    }
}