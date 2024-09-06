using System.Linq;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.UseCases.View.SelectSkillView.OutputBoundary;
using BattleScene.UseCases.View.SelectSkillView.OutputData;

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
                    x.Name,
                    x.Tp,
                    x.Disabled))
                .ToList();
            var highlightRow = outputData.Selection;
            var viewUpArrow = outputData.ListStart > 0;
            var viewDownArrow = outputData.ListStart < outputData.UpperLimit;

            _selectSkillView.StartAnimation(new SelectSkillViewDto(
                skillDtoList,
                highlightRow,
                viewUpArrow,
                viewDownArrow));
        }

        public void Stop()
        {
            _selectSkillView.StopAnimation();
        }
    }
}