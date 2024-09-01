using System;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Presenter.GridView;

namespace BattleScene.InterfaceAdapter.IView
{
    public interface IGridView
    {
        public Task StartAnimationAsync(GridViewDto dto);
        public void StopAnimation();
        public void SetSelectAction(Action<int> action);
    }
}