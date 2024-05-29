using System.Collections.Generic;
using BattleScene.UseCase.View.OrderView.OutputData;

namespace BattleScene.UseCase.View.OrderView.OutputBoundary
{
    public interface IOrderViewPresenter
    {
        public void Start(IList<OrderOutputData> orderOutputDataList);
    }
}