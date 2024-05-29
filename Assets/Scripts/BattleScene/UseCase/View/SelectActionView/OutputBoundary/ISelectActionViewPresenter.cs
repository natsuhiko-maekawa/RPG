using BattleScene.UseCase.View.SelectActionView.OutputData;

namespace BattleScene.UseCase.View.SelectActionView.OutputBoundary
{
    public interface ISelectActionViewPresenter
    {
        public void Start(SelectActionOutputData outputData);
        public void Stop();
    }
}