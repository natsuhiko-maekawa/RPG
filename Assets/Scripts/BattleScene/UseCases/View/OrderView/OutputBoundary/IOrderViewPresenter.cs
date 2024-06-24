using System.Collections.Generic;
using BattleScene.UseCases.View.OrderView.OutputData;

namespace BattleScene.UseCases.View.OrderView.OutputBoundary
{
    public interface IOrderViewPresenter
    {
        public void Start(IList<OrderOutputData> orderOutputDataList);
    }
}