using BattleScene.UseCases.View.TechnicalPointBarView.OutputData;

namespace BattleScene.UseCases.View.TechnicalPointBarView.OutputBoundary
{
    public interface ITechnicalPointBarViewPresenter
    {
        public void Start(TechnicalPointBarOutputData outputData);
    }
}