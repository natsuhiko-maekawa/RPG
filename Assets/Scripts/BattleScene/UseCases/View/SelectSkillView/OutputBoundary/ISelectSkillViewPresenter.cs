using BattleScene.UseCases.View.SelectSkillView.OutputData;

namespace BattleScene.UseCases.View.SelectSkillView.OutputBoundary
{
    public interface ISelectSkillViewPresenter
    {
        public void Start(SelectSkillOutputData outputData);
        public void Stop();
    }
}