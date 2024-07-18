using System;
using System.Collections.Generic;
using BattleScene.Domain.Entity;
using BattleScene.UseCases.View.OrderView.OutputData;

namespace BattleScene.UseCases.View.OrderView.OutputBoundary
{
    public interface IOrderViewPresenter
    {
        [Obsolete]
        public void Start(IList<OrderOutputData> orderOutputDataList);

        public void Start(IList<OrderedItemEntity> order);
    }
}