using BattleScene.UseCase.View.DestroyedPartView.OutputData;

namespace BattleScene.UseCase.View.DestroyedPartView.OutputBoundary
{
    public interface IDestroyedPartViewPresenter
    {
        public void Start(DestroyedPartOutputData outputData);
    }
}