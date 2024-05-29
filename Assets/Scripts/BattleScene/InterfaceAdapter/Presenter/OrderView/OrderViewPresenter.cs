using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase.View.OrderView.OutputBoundary;
using BattleScene.UseCase.View.OrderView.OutputData;
using static BattleScene.UseCase.View.OrderView.OutputData.OrderOutputDataType;

namespace BattleScene.InterfaceAdapter.Presenter.OrderView
{
    public class OrderViewPresenter : IOrderViewPresenter
    {
        private readonly IOrderView _orderView;

        public OrderViewPresenter(
            IOrderView orderView)
        {
            _orderView = orderView;
        }

        public void Start(IList<OrderOutputData> orderOutputDataList)
        {
            var orderViewDtoList = orderOutputDataList
                .Select(x =>
                {
                    return x.OrderOutputDataType switch
                    {
                        Player => new OrderViewDto(ItemType.Player, default, default),
                        Enemy => new OrderViewDto(ItemType.Enemy, x.ImagePath, default),
                        Ailment => new OrderViewDto(ItemType.Ailments, default, x.AilmentNumber),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                })
                .ToList();

            _orderView.StartAnimation(orderViewDtoList);
        }
    }
}