using System.Linq;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase.View.SelectSkillView.OutputBoundary;
using BattleScene.UseCase.View.SelectSkillView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.SelectSkillView
{
    public class SelectSkillViewPresenter : ISelectSkillViewPresenter
    {
        private readonly ISelectSkillView _selectSkillView;

        public SelectSkillViewPresenter(
            ISelectSkillView selectSkillView)
        {
            _selectSkillView = selectSkillView;
        }

        public void Start(SelectSkillOutputData outputData)
        {
            var skillDtoList = outputData.SkillList
                .Select(x => new SkillDto(
                    Name: x.Name,
                    Tp: x.Tp,
                    Disabled: x.Disabled))
                .ToList();
            var highlightRow = outputData.Selection;
            var viewUpArrow = outputData.ListStart > 0;
            var viewDownArrow = outputData.ListStart < outputData.UpperLimit;

            _selectSkillView.StartAnimation(new SelectSkillViewDto(
                SkillDtoList: skillDtoList,
                HighlightRow: highlightRow,
                ViewUpArrow: viewUpArrow,
                ViewDownArrow: viewDownArrow));
        }

        public void Stop()
        {
            _selectSkillView.StopAnimation();
        }
    }
}