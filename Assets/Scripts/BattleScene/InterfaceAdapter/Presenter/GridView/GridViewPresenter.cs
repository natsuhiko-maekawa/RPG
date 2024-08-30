using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCases.View;
using BattleScene.UseCases.View.GridView;

namespace BattleScene.InterfaceAdapter.Presenter.GridView
{
    public class GridViewPresenter : IViewPresenter<GridViewOutputData>
    {
        private readonly IGridView _gridView;
        
        public void Start(GridViewOutputData outputData)
        {
            var dto = new GridViewDto();
            _gridView.StartAnimationAsync(dto);
        }
    }
}