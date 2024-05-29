using System.Collections.Generic;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase;
using BattleScene.UseCase.View.IsContinueView.OutputBoundary;
using BattleScene.UseCase.View.IsContinueView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.SelectActionView
{
    public class IsContinueViewPresenter : IIsContinueViewPresenter
    {
        private readonly ISelectActionView _selectActionView;

        public IsContinueViewPresenter(
            ISelectActionView selectActionView)
        {
            _selectActionView = selectActionView;
        }
        
        public void Start(IsContinueOutputData outputData)
        {
            _selectActionView.StartAnimation(new SelectActionViewDto(
                ViewLength: outputData.ActualViewLength,
                TextList: Constant.OptionList,
                DisabledRowList: new List<int>(),
                HighlightRow: outputData.Selection));
        }

        public void Stop()
        {
            _selectActionView.StopAnimation();
        }
    }
}