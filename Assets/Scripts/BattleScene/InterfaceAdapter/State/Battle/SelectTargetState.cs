using BattleScene.UseCases.View;
using BattleScene.UseCases.View.GridView;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SelectTargetState : AbstractState
    {
        private readonly IViewPresenter<GridViewOutputData> _gridView;

        public SelectTargetState(
            IViewPresenter<GridViewOutputData> gridView)
        {
            _gridView = gridView;
        }

        public override void Start()
        {
            _gridView.Stop();
        }
    }
}