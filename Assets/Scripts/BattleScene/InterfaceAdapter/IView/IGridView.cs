using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.GridView;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface IGridView
    {
        public Task StartAnimationAsync(GridViewDto dto);
    }
}