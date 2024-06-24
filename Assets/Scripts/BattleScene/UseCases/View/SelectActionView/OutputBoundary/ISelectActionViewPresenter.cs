using BattleScene.UseCases.View.SelectActionView.OutputData;

namespace BattleScene.UseCases.View.SelectActionView.OutputBoundary
{
    public interface ISelectActionViewPresenter
    {
        public void Start(SelectActionOutputData outputData);
        public void Stop();
    }
}