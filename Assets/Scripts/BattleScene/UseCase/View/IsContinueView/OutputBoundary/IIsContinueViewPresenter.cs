using BattleScene.UseCase.View.IsContinueView.OutputData;

namespace BattleScene.UseCase.View.IsContinueView.OutputBoundary
{
    public interface IIsContinueViewPresenter
    {
        public void Start(IsContinueOutputData outputData);
        public void Stop();
    }
}