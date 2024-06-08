using BattleScene.UseCase.Output.Interface;
using BattleScene.UseCase.View.OrderView.OutputBoundary;
using BattleScene.UseCase.View.OrderView.OutputDataFactory;

namespace BattleScene.UseCase.View.OrderView
{
    internal class OrderViewOutput : IOutput
    {
        private readonly OrderOutputDataFactory _orderOutputDataFactory;
        private readonly IOrderViewPresenter _orderView;

        public OrderViewOutput(
            OrderOutputDataFactory orderOutputDataFactory, 
            IOrderViewPresenter orderView)
        {
            _orderOutputDataFactory = orderOutputDataFactory;
            _orderView = orderView;
        }

        public void Out()
        {
            _orderView.Start(_orderOutputDataFactory.Create());
        }
    }
}