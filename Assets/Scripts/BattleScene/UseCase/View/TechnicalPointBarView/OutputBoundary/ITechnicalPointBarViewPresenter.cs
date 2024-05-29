using BattleScene.UseCase.View.TechnicalPointBarView.OutputData;

namespace BattleScene.UseCase.View.TechnicalPointBarView.OutputBoundary
{
    public interface ITechnicalPointBarViewPresenter
    {
        public void Start(TechnicalPointBarOutputData outputData);
    }
}