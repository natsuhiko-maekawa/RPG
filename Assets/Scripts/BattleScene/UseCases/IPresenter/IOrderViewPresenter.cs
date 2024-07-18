using System.Collections.Generic;
using BattleScene.Domain.Entity;

namespace BattleScene.UseCases.View.OrderView.OutputBoundary
{
    public interface IOrderViewPresenter
    {
        public void Start(IList<OrderedItemEntity> order);
    }
}