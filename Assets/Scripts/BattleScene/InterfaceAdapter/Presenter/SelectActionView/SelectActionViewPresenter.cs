using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase;
using BattleScene.UseCase.View.SelectActionView.OutputBoundary;
using BattleScene.UseCase.View.SelectActionView.OutputData;

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
                ViewLength: outputData.ActualViewLength,
                TextList: Constant.ActionList,
                DisabledRowList: outputData.DisabledRowList,
                HighlightRow: outputData.Selection));
        }

        public void Stop()
        {
            _selectActionView.StopAnimation();   
        }
    }
}