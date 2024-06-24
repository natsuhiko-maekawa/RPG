using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCases;
using BattleScene.UseCases.View.SelectActionView.OutputBoundary;
using BattleScene.UseCases.View.SelectActionView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.SelectActionView
{
    public class SelectActionViewPresenter : ISelectActionViewPresenter
    {
        private readonly ISelectActionView _selectActionView;

        public SelectActionViewPresenter(
            ISelectActionView selectActionView)
        {
            _selectActionView = selectActionView;
        }

        public void Start(SelectActionOutputData outputData)
        {
            _selectActionView.StartAnimation(new SelectActionViewDto(
                outputData.ActualViewLength,
                Constant.ActionList,
                outputData.DisabledRowList,
                outputData.Selection));
        }

        public void Stop()
        {
            _selectActionView.StopAnimation();
        }
    }
}