using BattleScene.UseCases.View.DestroyedPartView.OutputData;

namespace BattleScene.UseCases.View.DestroyedPartView.OutputBoundary
{
    public interface IDestroyedPartViewPresenter
    {
        public void Start(DestroyedPartOutputData outputData);
    }
}