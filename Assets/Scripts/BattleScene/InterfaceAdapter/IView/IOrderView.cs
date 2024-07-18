using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.OrderView;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface IOrderView
    {
        public Task StartAnimation(IList<OrderViewDto> dtoList);
    }
}