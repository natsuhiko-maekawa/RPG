using BattleScene.UseCase.View.SelectSkillView.OutputData;

namespace BattleScene.UseCase.View.SelectSkillView.OutputBoundary
{
    public interface ISelectSkillViewPresenter
    {
        public void Start(SelectSkillOutputData outputData);
        public void Stop();
    }
}