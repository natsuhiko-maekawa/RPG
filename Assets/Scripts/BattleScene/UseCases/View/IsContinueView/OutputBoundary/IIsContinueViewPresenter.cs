using BattleScene.UseCases.View.IsContinueView.OutputData;

namespace BattleScene.UseCases.View.IsContinueView.OutputBoundary
{
    public interface IIsContinueViewPresenter
    {
        public void Start(IsContinueOutputData outputData);
        public void Stop();
    }
}