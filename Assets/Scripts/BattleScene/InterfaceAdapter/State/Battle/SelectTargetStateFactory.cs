using BattleScene.Domain.Code;
using BattleScene.UseCases.View;
using BattleScene.UseCases.View.GridView;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SelectTargetStateFactory
    {
        private readonly IViewPresenter<GridViewOutputData> _gridView;

        public SelectTargetStateFactory(
            IViewPresenter<GridViewOutputData> gridView)
        {
            _gridView = gridView;
        }
        
        public SelectTargetState Create(ActionCode actionCode)
        {
            return new SelectTargetState(_gridView);
        }
    }
}